using AutoMapper;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly IMapper _mapper;
    private readonly ISmsOrchestrationService _smsOrchestrationService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly IEmailTemplateService _emailTemplateService;

    public NotificationAggregatorService(
        IMapper mapper,
        ISmsTemplateService smsTemplateService,
        IEmailTemplateService emailTemplateService,
        ISmsOrchestrationService smsOrchestrationService,
        IEmailOrchestrationService emailOrchestrationService
    )
    {
        _mapper = mapper;
        _smsOrchestrationService = smsOrchestrationService;
        _emailOrchestrationService = emailOrchestrationService;
        _smsTemplateService = smsTemplateService;
        _emailTemplateService = emailTemplateService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var sendNotificationTask = async () =>
        {
            // check notification type - from parameters

            // check notification type - from user settings

            // check notification type - external configuration

            var sendNotificationTask = notificationRequest.Type switch
            {
                NotificationType.Sms => _smsOrchestrationService.SendAsync(
                    _mapper.Map<SmsNotificationRequest>(notificationRequest),
                    cancellationToken),
                NotificationType.Email => _emailOrchestrationService.SendAsync(
                    _mapper.Map<EmailNotificationRequest>(notificationRequest),
                    cancellationToken),
                _ => throw new NotImplementedException("This type of notification is not supported yet.")
            };

            var sendNotificationResult = await sendNotificationTask;
            return sendNotificationResult.Data;
        };

        return await sendNotificationTask.GetValueAsync();
    }

    public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var templates = new List<NotificationTemplate>();

        if (filter.TemplateType.Contains(NotificationType.Sms))
            templates.AddRange(await _smsTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

        if (filter.TemplateType.Contains(NotificationType.Email))
            templates.AddRange(await _emailTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

        return templates;
    }
}