using Notifications.Infrastructure.Domain.Enums;
using NotificationTemplateType = Notifications.Infrastructure.Application.Common.Enums.NotificationTemplateType;

namespace Notifications.Infrastructure.Application.Common.Notifications.Models;

public class NotificationRequest
{
    public Guid ReceiverId { get; set; }
    
    public NotificationTemplateType TemplateType { get; set; }

    public Dictionary<string, string> Variables { get; set; }
    
    public NotificationType? NotificationType { get; set; } = null;
    
    public Guid? SenderId { get; set; } = null;
}