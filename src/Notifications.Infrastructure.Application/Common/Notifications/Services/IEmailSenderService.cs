using Notifications.Infrastructure.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}