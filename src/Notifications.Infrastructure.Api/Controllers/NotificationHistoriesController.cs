﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationHistoriesController : ControllerBase
{
    [HttpGet("sms")]
    public async ValueTask<IActionResult> Get([FromServices] IEmailHistoryRepository repo) =>
        Ok(await repo.Get().ToListAsync());

    [HttpGet("email")]
    public async ValueTask<IActionResult> Get([FromServices] ISmsHistoryRepository repo) =>
        Ok(await repo.Get().ToListAsync());
}