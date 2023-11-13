namespace Notifications.Infrastructure.Application.Common.Notifications.Brokers;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken
    );
}