using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exceptions;
using Notifications.Infrastructure.Domain.Entities;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IEmailRenderingService _emailRenderingService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IEmailHistoryService _emailHistoryService;

    public EmailOrchestrationService(
        IMapper mapper,
        IEmailTemplateService emailTemplateService,
        IEmailRenderingService emailRenderingService,
        IEmailSenderService emailSenderService,
        IEmailHistoryService emailHistoryService
    )
    {
        _mapper = mapper;
        _emailTemplateService = emailTemplateService;
        _emailRenderingService = emailRenderingService;
        _emailSenderService = emailSenderService;
        _emailHistoryService = emailHistoryService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        EmailNotificationRequest request,
        CancellationToken cancellationToken = default
    )
    {
        // validate

        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<EmailMessage>(request);

            // get template
            message.Template = await _emailTemplateService.GetByTypeAsync(request.TemplateType, true, cancellationToken);

            // render template
            await _emailRenderingService.RenderAsync(message, cancellationToken);

            // send message
            await _emailSenderService.SendAsync(message, cancellationToken);

            // save history
            var history = _mapper.Map<EmailHistory>(message);
            await _emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await sendNotificationRequest.GetValueAsync();
    }
}