using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Domain.Extensions;
using NotificationTemplateType = Notifications.Infrastructure.Application.Common.Enums.NotificationTemplateType;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly ISmsOrchestrationService _smsOrchestrationService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly IEmailTemplateService _emailTemplateService;

    public NotificationAggregatorService(
        ISmsOrchestrationService smsOrchestrationService,
        ISmsTemplateService smsTemplateService,
        IEmailTemplateService emailTemplateService
    )
    {
        _smsOrchestrationService = smsOrchestrationService;
        _smsTemplateService = smsTemplateService;
        _emailTemplateService = emailTemplateService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var test = async () =>
        {
            // validate - if user exists

            // if sender is not passed - use system account

            var senderUser = new User
            {
                PhoneNumber = "+12132931337"
            };

            var receiverUser = new User
            {
                PhoneNumber = "+998999663258"
            };

            // check notification type - from parameters

            // check notification type - from user settings

            // check notification type - external configuration

            // save history

            var sendNotificationTask = notificationRequest.NotificationType switch
            {
                NotificationType.Sms => SendSmsAsync(senderUser,
                    receiverUser,
                    notificationRequest.TemplateType,
                    notificationRequest.Variables,
                    cancellationToken),
                _ => throw new NotImplementedException("Email is not supported yet.")
            };

            var sendNotificationResult = await sendNotificationTask;
            return sendNotificationResult.Data;
        };

        return await test.GetValueAsync();
    }

    public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var templates = new List<NotificationTemplate>();

        if (filter.TemplateType.Contains(NotificationType.Sms))
            templates.AddRange(await _smsTemplateService.GetByFilterAsync(filter));

        if (filter.TemplateType.Contains(NotificationType.Email))
            templates.AddRange(await _emailTemplateService.GetByFilterAsync(filter));

        return templates;
    }

    private async ValueTask<FuncResult<bool>> SendSmsAsync(
        User senderUser,
        User receiverUser,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
    )
    {
        // get user phone number

        // send sms

        return await _smsOrchestrationService.SendAsync(senderUser.PhoneNumber,
            receiverUser.PhoneNumber,
            templateType,
            variables,
            cancellationToken);
    }
}