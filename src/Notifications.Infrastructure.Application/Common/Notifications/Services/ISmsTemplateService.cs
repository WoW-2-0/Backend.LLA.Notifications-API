using System.Linq.Expressions;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsTemplateService
{
    IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<IList<SmsTemplate>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}