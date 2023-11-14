﻿namespace Notifications.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder.AddValidators().AddNotificationInfrastructure().AddExposers().AddDevTools();

        return new(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app.UseExposers().UseDevTools();

        return new(app);
    }
}