using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces;

public interface IUserSettingsRepository
{
    ValueTask<UserSettings?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );
}