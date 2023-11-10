namespace Sms.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken
    );
}