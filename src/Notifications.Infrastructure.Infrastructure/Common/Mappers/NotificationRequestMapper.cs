using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Mappers;

public class NotificationRequestMapper : Profile
{
    public NotificationRequestMapper()
    {
        CreateMap<NotificationRequest, EmailNotificationRequest>();
        CreateMap<NotificationRequest, SmsNotificationRequest>();
    }
}