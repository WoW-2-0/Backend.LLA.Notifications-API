using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationHistoryAndNotificationTemplateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistories_TemplateId",
                table: "NotificationHistories",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistories_NotificationTemplates_TemplateId",
                table: "NotificationHistories",
                column: "TemplateId",
                principalTable: "NotificationTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistories_NotificationTemplates_TemplateId",
                table: "NotificationHistories");

            migrationBuilder.DropIndex(
                name: "IX_NotificationHistories_TemplateId",
                table: "NotificationHistories");
        }
    }
}
