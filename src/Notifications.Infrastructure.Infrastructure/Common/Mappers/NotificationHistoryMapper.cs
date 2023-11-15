﻿using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Mappers;

public class NotificationHistoryMapper : Profile
{
    public NotificationHistoryMapper()
    {
        CreateMap<EmailMessage, EmailHistory>();
        CreateMap<SmsMessage, SmsHistory>();
    }
}