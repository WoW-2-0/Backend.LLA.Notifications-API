using Notifications.Infrastructure.Domain.Common.Entities;

namespace Notifications.Infrastructure.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;
}