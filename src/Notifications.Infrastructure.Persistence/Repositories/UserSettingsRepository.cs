using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Persistence.DataContexts;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories;

public class UserSettingsRepository : EntityRepositoryBase<UserSettings, NotificationDbContext>, IUserSettingsRepository
{
    public UserSettingsRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public ValueTask<UserSettings?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdAsync(userId, asNoTracking, cancellationToken);
}