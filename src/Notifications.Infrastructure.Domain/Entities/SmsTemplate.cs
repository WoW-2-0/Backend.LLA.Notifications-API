using Type = Notifications.Infrastructure.Domain.Enums.NotificationType;

namespace Notifications.Infrastructure.Domain.Entities;

public class SmsTemplate : NotificationTemplate
{
    public SmsTemplate()
    {
        Type = Type.Sms;
    }
}