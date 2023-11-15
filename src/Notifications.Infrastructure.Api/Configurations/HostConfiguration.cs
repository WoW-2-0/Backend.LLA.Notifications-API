namespace Notifications.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder.AddMappers()
            .AddValidators()
            .AddIdentityInfrastructure()
            .AddNotificationInfrastructure()
            .AddExposers()
            .AddDevTools();

        return new(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app.UseExposers().UseDevTools();

        return new(app);
    }
}