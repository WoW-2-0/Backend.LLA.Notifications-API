using System.ComponentModel;

namespace Notifications.Infrastructure.Domain.Enums;

public enum NotificationTemplateType
{
    SystemWelcomeNotification = 0,
    EmailAddressVerificationNotification = 1,
    PhoneNumberVerificationNotification = 2,
    ReferralNotification = 3,
}