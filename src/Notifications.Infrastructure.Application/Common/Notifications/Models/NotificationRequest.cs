using Notifications.Infrastructure.Domain.Enums;
using NotificationTemplateType = Notifications.Infrastructure.Application.Common.Enums.NotificationTemplateType;

namespace Notifications.Infrastructure.Application.Common.Notifications.Models;

public class NotificationRequest
{
    public Guid? SenderUserId { get; set; } = null;
    
    public Guid ReceiverUserId { get; set; }

    public NotificationTemplateType TemplateType { get; set; }

    public NotificationType? Type { get; set; } = null;

    public Dictionary<string, string>? Variables { get; set; }
}