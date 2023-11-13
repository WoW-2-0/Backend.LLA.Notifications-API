using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Notifications.Brokers;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Brokers;
using Notifications.Infrastructure.Infrastrucutre.Common.Notifications.Services;
using Notifications.Infrastructure.Persistence.DataContexts;
using Notifications.Infrastructure.Persistence.Repositories;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());

        builder.Services.AddValidatorsFromAssemblies(assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register persistence
        builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("NotificationsDatabaseConnection")));

        builder.Services
            .AddScoped<ISmsTemplateRepository, SmsTemplateRepository>()
            .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        
        // register brokers
        builder.Services.AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

        // register data access foundation services
        builder.Services
            .AddScoped<ISmsTemplateService, SmsTemplateService>()
            .AddScoped<IEmailTemplateService, EmailTemplateService>();
        
        // register helper foundation services
        builder.Services.AddScoped<ISmsSenderService, SmsSenderService>();

        // register orchestration and aggregation services
        builder.Services.AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}