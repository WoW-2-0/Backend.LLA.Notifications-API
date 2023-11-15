using Microsoft.AspNetCore.Mvc;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationAggregatorService _notificationAggregatorService;

    public NotificationsController(INotificationAggregatorService notificationAggregatorService)
    {
        _notificationAggregatorService = notificationAggregatorService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Send([FromBody] NotificationRequest request)
    {
        var result = await _notificationAggregatorService.SendAsync(request);
        return result.IsSuccess && (result?.Data ?? false) ? Ok() : BadRequest();
    }

    [HttpGet("templates")]
    public async ValueTask<IActionResult> GetTemplates(
        [FromQuery] NotificationTemplateFilter filter,
        CancellationToken cancellationToken
    )
    {
        var result = await _notificationAggregatorService.GetTemplatesByFilterAsync(filter, cancellationToken);
        return result.Any() ? Ok(result) : BadRequest();
    }

    [HttpGet("templates/sms")]
    public async ValueTask<IActionResult> GetSmsTemplates(
        [FromQuery] FilterPagination filter,
        [FromServices] ISmsTemplateService smsTemplateService,
        bool cancellationToken
    )
    {
        var result = await smsTemplateService.GetByFilterAsync(filter, cancellationToken);
        return result.Any() ? Ok(result) : BadRequest();
    }

    [HttpGet("templates/email")]
    public async ValueTask<IActionResult> GetEmailTemplates(
        [FromQuery] FilterPagination filter,
        [FromServices] IEmailTemplateService emailTemplateService,
        bool cancellationToken
    )
    {
        var result = await emailTemplateService.GetByFilterAsync(filter, cancellationToken);
        return result.Any() ? Ok(result) : BadRequest();
    }

    [HttpPost("templates/sms")]
    public async ValueTask<IActionResult> CreateSmsTemplate(
        [FromBody] SmsTemplate template,
        [FromServices] ISmsTemplateService smsTemplateService,
        CancellationToken cancellationToken
    )
    {
        var result = await smsTemplateService.CreateAsync(template, cancellationToken: cancellationToken);
        return Ok(result);
    }

    [HttpPost("templates/email")]
    public async ValueTask<IActionResult> CreateEmailTemplate(
        [FromBody] EmailTemplate template,
        [FromServices] IEmailTemplateService emailTemplateService,
        CancellationToken cancellationToken
    )
    {
        var result = await emailTemplateService.CreateAsync(template, cancellationToken: cancellationToken);
        return Ok(result);
    }
}