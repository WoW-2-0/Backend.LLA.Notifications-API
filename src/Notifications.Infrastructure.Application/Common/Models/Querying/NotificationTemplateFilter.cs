using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Models.Querying;

public class NotificationTemplateFilter : FilterPagination
{
    public IList<NotificationType> TemplateType { get; set; }
}