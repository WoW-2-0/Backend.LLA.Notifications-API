using Sms.Infrastructure.Application.Common.Enums;
using Sms.Infrastructure.Application.Common.Notifications.Models;
using Sms.Infrastructure.Application.Common.Notifications.Services;
using Sms.Infrastructure.Domain.Common.Exceptions;
using Sms.Infrastructure.Domain.Entities;
using Sms.Infrastructure.Domain.Extensions;

namespace Sms.Infrastructure.Infrastructure.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly ISmsOrchestrationService _smsOrchestrationService;

    public NotificationAggregatorService(ISmsOrchestrationService smsOrchestrationService)
    {
        _smsOrchestrationService = smsOrchestrationService;
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