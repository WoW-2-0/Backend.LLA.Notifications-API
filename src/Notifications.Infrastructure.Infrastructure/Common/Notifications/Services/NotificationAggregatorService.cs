using AutoMapper;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Domain.Extensions;
using Notifications.Infrastructure.Infrastrucutre.Common.Settings;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly IMapper _mapper;
    private readonly IOptions<NotificationSettings> _notificationSettings;
    private readonly ISmsOrchestrationService _smsOrchestrationService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly IUserService _userService;
    private readonly IUserSettingsService _userSettingsService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly IEmailTemplateService _emailTemplateService;

    public NotificationAggregatorService(
        IMapper mapper,
        IOptions<NotificationSettings> notificationSettings,
        ISmsTemplateService smsTemplateService,
        IEmailTemplateService emailTemplateService,
        ISmsOrchestrationService smsOrchestrationService,
        IEmailOrchestrationService emailOrchestrationService,
        IUserSettingsService userSettingsService,
        IUserService userService
    )
    {
        _mapper = mapper;
        _notificationSettings = notificationSettings;
        _smsOrchestrationService = smsOrchestrationService;
        _emailOrchestrationService = emailOrchestrationService;
        _smsTemplateService = smsTemplateService;
        _emailTemplateService = emailTemplateService;
        _userSettingsService = userSettingsService;
        _userService = userService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var sendNotificationTask = async () =>
        {
            // If sender is not specified, get system user
            var senderUser = notificationRequest.SenderUserId.HasValue
                ? await _userService.GetByIdAsync(notificationRequest.SenderUserId.Value,
                    cancellationToken: cancellationToken)
                : await _userService.GetSystemUserAsync(true, cancellationToken);

            notificationRequest.SenderUserId = senderUser!.Id;

            var receiverUser = await _userService.GetByIdAsync(notificationRequest.ReceiverUserId,
                cancellationToken: cancellationToken);

            // If notification provider type is not specified, get from receiver user settings
            if (!notificationRequest.Type.HasValue && receiverUser!.UserSettings.PreferredNotificationType.HasValue)
                notificationRequest.Type = receiverUser!.UserSettings.PreferredNotificationType!.Value;

            // If user not specified preferred notification type get from settings
            if (!notificationRequest.Type.HasValue)
                notificationRequest.Type = _notificationSettings.Value.DefaultNotificationType;

            var sendNotificationTask = notificationRequest.Type switch
            {
                NotificationType.Sms => _smsOrchestrationService.SendAsync(
                    _mapper.Map<SmsNotificationRequest>(notificationRequest),
                    cancellationToken),
                NotificationType.Email => _emailOrchestrationService.SendAsync(
                    _mapper.Map<EmailNotificationRequest>(notificationRequest),
                    cancellationToken),
                _ => throw new NotImplementedException("This type of notification is not supported yet.")
            };

            var sendNotificationResult = await sendNotificationTask;
            return sendNotificationResult.Data;
        };

        return await sendNotificationTask.GetValueAsync();
    }

    public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var templates = new List<NotificationTemplate>();

        if (filter.TemplateType.Contains(NotificationType.Sms))
            templates.AddRange(
                await _smsTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

        if (filter.TemplateType.Contains(NotificationType.Email))
            templates.AddRange(
                await _emailTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

        return templates;
    }
}