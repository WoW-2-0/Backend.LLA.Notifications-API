using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entities;

namespace Notifications.Infrastructure.Persistence.DataContexts;

public class NotificationDbContext : DbContext
{
    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<SmsHistory> SmsHistories => Set<SmsHistory>();
    
    public DbSet<EmailHistory> EmailHistories => Set<EmailHistory>();
    
    public DbSet<User> Users => Set<User>();

    public DbSet<UserSettings> UserSettings => Set<UserSettings>();

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}