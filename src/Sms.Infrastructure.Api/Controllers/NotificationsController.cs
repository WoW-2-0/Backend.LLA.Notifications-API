using Microsoft.AspNetCore.Mvc;
using Sms.Infrastructure.Application.Common.Notifications.Models;
using Sms.Infrastructure.Application.Common.Notifications.Services;

namespace Sms.Infrastructure.Api.Controllers;

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
    public async ValueTask<IActionResult> Send([FromBody]NotificationRequest request)
    {
        var result = await _notificationAggregatorService.SendAsync(request);
        return result.IsSuccess ? Ok() : BadRequest();
    }
}