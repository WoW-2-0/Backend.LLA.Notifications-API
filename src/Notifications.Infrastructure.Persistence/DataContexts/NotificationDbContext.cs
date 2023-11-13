using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Persistence.DataContexts;

public class NotificationDbContext : DbContext
{
    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();
    
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}