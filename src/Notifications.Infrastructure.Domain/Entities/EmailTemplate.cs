using Type = Notifications.Infrastructure.Domain.Enums.NotificationType;

namespace Notifications.Infrastructure.Domain.Entities;

public class EmailTemplate : NotificationTemplate
{
    public EmailTemplate()
    {
        NotificationType = Type.Email;
    }

    public string Subject { get; set; } = default!;
}