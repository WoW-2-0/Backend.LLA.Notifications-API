using Notifications.Infrastructure.Application.Common.Notifications.Brokers;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers)
    {
        _emailSenderBrokers = emailSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        foreach (var smsSenderBroker in _emailSenderBrokers)
        {
            var sendNotificationTask = () => smsSenderBroker.SendAsync(emailMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            emailMessage.IsSuccessful = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;
            return result.IsSuccess;
        }

        return false;
    }
}