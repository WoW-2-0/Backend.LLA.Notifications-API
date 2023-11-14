using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Application.Common.Notifications.Brokers;
using Notifications.Infrastructure.Infrastrucutre.Common.Settings;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Brokers;

public class SmtpEmailSenderBroker : IEmailSenderBroker
{
    private readonly SmtpEmailSenderSettings _smtpEmailSenderSettings;

    public SmtpEmailSenderBroker(IOptions<SmtpEmailSenderSettings> smtpEmailSenderSettings)
    {
        _smtpEmailSenderSettings = smtpEmailSenderSettings.Value;
    }

    public ValueTask<bool> SendAsync(
        string senderEmailAddress,
        string receiverEmailAddress,
        string subject,
        string body,
        CancellationToken cancellationToken = default
    )
    {
        senderEmailAddress ??= _smtpEmailSenderSettings.CredentialAddress;

        var mail = new MailMessage(senderEmailAddress, receiverEmailAddress);
        mail.Subject = subject;
        mail.Body = body;

        var smtpClient = new SmtpClient(_smtpEmailSenderSettings.Host, _smtpEmailSenderSettings.Port);
        smtpClient.Credentials =
            new NetworkCredential(_smtpEmailSenderSettings.CredentialAddress, _smtpEmailSenderSettings.Password);
        smtpClient.EnableSsl = true;

        smtpClient.Send(mail);

        return new(true);
    }
}