using Notifications.Infrastructure.Application.Common.Notifications.Brokers;
using Notifications.Infrastructure.Application.Common.Notifications.Services;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBrokers)
    {
        _smsSenderBrokers = smsSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken
    )
    {
        var result = false;

        foreach (var smsSenderBroker in _smsSenderBrokers)
        {
            try
            {
                result = await smsSenderBroker.SendAsync(senderPhoneNumber,
                    receiverPhoneNumber,
                    message,
                    cancellationToken);

                if (result) return result;
            }
            catch (Exception e)
            {
                // TODO : log exception
            }
        }

        return result;
    }
}