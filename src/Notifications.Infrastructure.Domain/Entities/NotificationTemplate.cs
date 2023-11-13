using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entities;

// Sms Template
// Email Template

public abstract class NotificationTemplate : IEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = default!;

    public NotificationType NotificationType { get; set; }
}