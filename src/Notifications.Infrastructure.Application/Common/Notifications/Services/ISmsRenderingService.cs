using Notifications.Infrastructure.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsRenderingService
{
    ValueTask<string> RenderAsync(
        SmsMessage smsMessage,
        CancellationToken cancellationToken = default
    );
}