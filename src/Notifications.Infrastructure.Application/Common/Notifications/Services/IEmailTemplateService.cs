using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailTemplateService
{
    ValueTask<IList<EmailTemplate>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<EmailTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}