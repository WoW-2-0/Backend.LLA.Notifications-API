using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Notifications.Models;

public class SmsNotificationRequest : NotificationRequest
{
    public SmsNotificationRequest() => Type = NotificationType.Sms;
}