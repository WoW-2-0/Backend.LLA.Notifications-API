using Sms.Infrastructure.Application.Common.Notifications.Brokers;
using Sms.Infrastructure.Application.Common.Notifications.Services;
using Sms.Infrastructure.Infrastructure.Common.Notifications.Brokers;
using Sms.Infrastructure.Infrastructure.Common.Notifications.Services;

namespace Sms.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register brokers
        builder.Services.AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

        // register data access foundation services

        // register helper foundation services
        builder.Services.AddScoped<ISmsSenderService, SmsSenderService>();
        
        // register orchestration and aggregation services
        builder.Services
            .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}