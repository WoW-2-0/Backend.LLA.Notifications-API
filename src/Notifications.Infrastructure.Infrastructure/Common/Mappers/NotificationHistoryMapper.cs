using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Mappers;

public class NotificationHistoryMapper : Profile
{
    public NotificationHistoryMapper()
    {
        CreateMap<EmailMessage, EmailHistory>()
            .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => src.Template.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Body));

        CreateMap<SmsMessage, SmsHistory>()
            .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => src.Template.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message));
    }
}