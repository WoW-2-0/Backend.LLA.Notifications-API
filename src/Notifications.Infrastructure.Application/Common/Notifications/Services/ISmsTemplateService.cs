using System.Linq.Expressions;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsTemplateService
{
    ValueTask<IList<SmsTemplate>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );
    
    ValueTask<SmsTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}