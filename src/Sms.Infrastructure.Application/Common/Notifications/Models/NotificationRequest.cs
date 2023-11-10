using Sms.Infrastructure.Application.Common.Enums;

namespace Sms.Infrastructure.Application.Common.Notifications.Models;

public class NotificationRequest
{
    public Guid ReceiverId { get; set; }
    
    public NotificationTemplateType TemplateType { get; set; }

    public Dictionary<string, string> Variables { get; set; }
    
    public NotificationType? NotificationType { get; set; } = null;
    
    public Guid? SenderId { get; set; } = null;
}