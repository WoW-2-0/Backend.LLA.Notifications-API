using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entities;

public class EmailHistory : NotificationHistory
{
    public EmailHistory()
    {
        Type = NotificationType.Email;
    }

    public string SendEmailAddress { get; set; } = default!;

    public string ReceiverEmailAddress { get; set; } = default!;

    public string Subject { get; set; } = default!;
    
    // TODO : check if that works
    // public EmailTemplate EmailTemplate => Template as EmailTemplate;
}