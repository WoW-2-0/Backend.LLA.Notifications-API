using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Mappers;

public class NotificationMessageMapper : Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<EmailNotificationRequest, EmailMessage>();
        CreateMap<SmsNotificationRequest, SmsMessage>();
    }
}