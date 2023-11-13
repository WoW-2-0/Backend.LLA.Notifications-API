using FluentValidation;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
{
    public EmailTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotEmpty()
            // .WithMessage("Sms template content is required")
            .MinimumLength(10)
            // .WithMessage("Sms template content must be at least 10 characters long")
            .MaximumLength(256);
            // .WithMessage("Sms template content must be at most 256 characters long");

        RuleFor(template => template.NotificationType)
            .Equal(NotificationType.Email);
            // .WithMessage("Sms template notification type must be Sms");
    }
}