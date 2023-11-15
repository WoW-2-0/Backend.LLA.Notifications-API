using Notifications.Infrastructure.Application.Common.Notifications.Brokers;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBrokers)
    {
        _smsSenderBrokers = smsSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        foreach (var smsSenderBroker in _smsSenderBrokers)
        {
            var sendNotificationTask = () => smsSenderBroker.SendAsync(smsMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            // TODO : check later
            return result.IsSuccess;
        }

        return false;
    }
}