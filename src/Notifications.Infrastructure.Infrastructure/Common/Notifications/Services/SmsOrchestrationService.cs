using AutoMapper;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Domain.Extensions;
using Notifications.Infrastructure.Persistence.DataContexts;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly ISmsSenderService _smsSenderService;
    private readonly ISmsHistoryService _smsHistoryService;
    private readonly NotificationDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly ISmsRenderingService _smsRenderingService;

    public SmsOrchestrationService(
        IMapper mapper,
        ISmsTemplateService smsTemplateService,
        ISmsRenderingService smsRenderingService,
        ISmsSenderService smsSenderService,
        ISmsHistoryService smsHistoryService,
        NotificationDbContext dbContext,
        IUserService userService
    )
    {
        _mapper = mapper;
        _smsTemplateService = smsTemplateService;
        _smsRenderingService = smsRenderingService;
        _smsSenderService = smsSenderService;
        _smsHistoryService = smsHistoryService;
        _dbContext = dbContext;
        _userService = userService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        SmsNotificationRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<SmsMessage>(request);

            // get users
            // set receiver phone number and sender phone number
            var senderUser =
                (await _userService.GetByIdAsync(request.SenderUserId!.Value, cancellationToken: cancellationToken))!;

            var receiverUser =
                (await _userService.GetByIdAsync(request.ReceiverUserId, cancellationToken: cancellationToken))!;

            message.SenderPhoneNumber = senderUser.PhoneNumber;
            message.ReceiverPhoneNumber = receiverUser.PhoneNumber;

            // get template
            message.Template =
                await _smsTemplateService.GetByTypeAsync(request.TemplateType, true, cancellationToken) ??
                throw new InvalidOperationException(
                    $"Invalid template for sending {NotificationType.Sms} notification");

            // blogs.Comments.Add(new Comment { Title = "My comment" });

            // render template
            await _smsRenderingService.RenderAsync(message, cancellationToken);

            // send message
            await _smsSenderService.SendAsync(message, cancellationToken);

            // save history

            var history = _mapper.Map<SmsHistory>(message);
            var test = _dbContext.Entry(history.Template).State;

            await _smsHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await sendNotificationRequest.GetValueAsync();
    }
}