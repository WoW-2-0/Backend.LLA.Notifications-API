using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Application.Common.Querying.Extensions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class SmsHistoryService : ISmsHistoryService
{
    private readonly ISmsHistoryRepository _smsHistoryRepository;

    public SmsHistoryService(ISmsHistoryRepository smsHistoryRepository)
    {
        _smsHistoryRepository = smsHistoryRepository;
    }

    public async ValueTask<IList<SmsHistory>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        await _smsHistoryRepository.Get().ApplyPagination(paginationOptions).ToListAsync(cancellationToken);

    public async ValueTask<SmsHistory> CreateAsync(
        SmsHistory smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    ) =>
        await _smsHistoryRepository.CreateAsync(smsTemplate, saveChanges, cancellationToken);
}