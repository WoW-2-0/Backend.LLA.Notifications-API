using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Persistence.EntityConfigurations;

public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
{
    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder.Property(template => template.Content).HasMaxLength(256);
        
        builder
            .HasDiscriminator(emailTemplate => emailTemplate.NotificationType)
            .HasValue<SmsTemplate>(NotificationType.Sms)
            .HasValue<EmailTemplate>(NotificationType.Email);
    }
}