using System.Text;
using System.Text.RegularExpressions;
using Notifications.Infrastructure.Application.Common.Enums;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly ISmsSenderService _smsSenderService;

    public SmsOrchestrationService(ISmsSenderService smsSenderService)
    {
        _smsSenderService = smsSenderService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
    )
    {
        // validate

        var test = async () =>
        {
            // get template

            // render template

            // send message
            var template = GetTemplate(templateType);


            var message = GetMessage(template, variables);


            await _smsSenderService.SendAsync(senderPhoneNumber, receiverPhoneNumber, message, cancellationToken);

            return true;

            // save history
        };

        return await test.GetValueAsync();
    }

    public string GetTemplate(NotificationTemplateType templateType)
    {
        var template = templateType switch
        {
            NotificationTemplateType.SystemWelcomeNotification => "Welcome to the system, {{UserName}}",
            NotificationTemplateType.EmailVerificationNotification => "Verify your email by clicking the link, {{VerificationLink}}",
            _ => throw new ArgumentOutOfRangeException(nameof(templateType), "")
        };

        return template;
    }

    public string GetMessage(string template, Dictionary<string, string> variables)
    {
        // TODO : validate variables with messages

        var messageBuilder = new StringBuilder(template);

        var pattern = @"\{\{([^\{\}]+)\}\}";
        var matchValuePattern = "{{(.*?)}}";
        var matches = Regex.Matches(template, pattern)
            .Select(match =>
            {
                var placeholder = match.Value;
                var placeholderValue = Regex.Match(placeholder, matchValuePattern).Groups[1].Value;
                var valid = variables.TryGetValue(placeholderValue, out var value);

                return new
                {
                    Placeholder = placeholder,
                    Value = value,
                    IsValid = valid
                };
            });

        foreach (var match in matches)
            messageBuilder.Replace(match.Placeholder, match.Value);

        var message = messageBuilder.ToString();
        return message;
    }
}