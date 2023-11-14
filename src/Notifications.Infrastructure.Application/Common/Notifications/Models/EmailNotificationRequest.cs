using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Notifications.Models;

public class EmailNotificationRequest : NotificationRequest
{
    public EmailNotificationRequest() => Type = NotificationType.Email;

    // attachments etc.
}