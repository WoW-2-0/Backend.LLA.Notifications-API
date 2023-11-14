using Notifications.Infrastructure.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Application.Common.Notifications.Brokers;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}