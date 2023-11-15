using System.Linq.Expressions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Persistence.DataContexts;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories;

public class UserRepository : EntityRepositoryBase<User, NotificationDbContext>, IUserRepository
{
    public UserRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false)
     => base.Get(predicate, asNoTracking);

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> usersId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdsAsync(usersId, asNoTracking, cancellationToken);

    public ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        base.GetByIdAsync(userId, asNoTracking, cancellationToken);
}