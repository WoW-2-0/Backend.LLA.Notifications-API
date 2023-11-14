namespace Notifications.Infrastructure.Application.Common.Notifications.Brokers;

public interface IEmailSenderBroker
{
    ValueTask<bool> SendAsync(
        string senderEmailAddress,
        string receiverEmailAddress,
        string subject,
        string body,
        CancellationToken cancellationToken = default
    );
}