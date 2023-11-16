using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationHistoryAndUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistories_ReceiverUserId",
                table: "NotificationHistories",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistories_SenderUserId",
                table: "NotificationHistories",
                column: "SenderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistories_Users_ReceiverUserId",
                table: "NotificationHistories",
                column: "ReceiverUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistories_Users_SenderUserId",
                table: "NotificationHistories",
                column: "SenderUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistories_Users_ReceiverUserId",
                table: "NotificationHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistories_Users_SenderUserId",
                table: "NotificationHistories");

            migrationBuilder.DropIndex(
                name: "IX_NotificationHistories_ReceiverUserId",
                table: "NotificationHistories");

            migrationBuilder.DropIndex(
                name: "IX_NotificationHistories_SenderUserId",
                table: "NotificationHistories");
        }
    }
}
