using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNotificationTemplateTypeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotificationTemplates_TemplateType",
                table: "NotificationTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_TemplateType",
                table: "NotificationTemplates",
                column: "TemplateType",
                unique: true);
        }
    }
}
