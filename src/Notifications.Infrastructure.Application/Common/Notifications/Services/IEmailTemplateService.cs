using System.Linq.Expressions;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailTemplateService
{
    IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false
    );

    ValueTask<IList<EmailTemplate>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}