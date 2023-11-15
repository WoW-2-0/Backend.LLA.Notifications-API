using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToNotificationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateType",
                table: "NotificationTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "NotificationHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "NotificationHistories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_TemplateType",
                table: "NotificationTemplates",
                column: "TemplateType",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotificationTemplates_TemplateType",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateType",
                table: "NotificationTemplates");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "NotificationHistories");

            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "NotificationHistories");
        }
    }
}
