using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Settings;

public class TemplateRenderingSettings
{
    public string PlaceholderRegexPattern { get; set; } = default!;

    public string PlaceholderValueRegexPattern { get; set; } = default!;
    
    public int RegexMatchTimeoutInSeconds { get; set; }
}