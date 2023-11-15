using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly ISmsSenderService _smsSenderService;
    private readonly ISmsHistoryService _smsHistoryService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly ISmsRenderingService _smsRenderingService;

    public SmsOrchestrationService(
        IMapper mapper,
        ISmsTemplateService smsTemplateService,
        ISmsRenderingService smsRenderingService,
        ISmsSenderService smsSenderService,
        ISmsHistoryService smsHistoryService
    )
    {
        _mapper = mapper;
        _smsTemplateService = smsTemplateService;
        _smsRenderingService = smsRenderingService;
        _smsSenderService = smsSenderService;
        _smsHistoryService = smsHistoryService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        SmsNotificationRequest request,
        CancellationToken cancellationToken = default
    )
    {
        // validate

        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<SmsMessage>(request);

            // get template
            message.Template = await _smsTemplateService.GetByTypeAsync(request.TemplateType, true, cancellationToken);

            // render template
            await _smsRenderingService.RenderAsync(message, cancellationToken);

            // send message
            await _smsSenderService.SendAsync(message, cancellationToken);

            // save history
            var history = _mapper.Map<SmsHistory>(message);
            await _smsHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await sendNotificationRequest.GetValueAsync();
    }
}