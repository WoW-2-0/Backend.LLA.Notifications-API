﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationHistoryAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationType",
                table: "NotificationTemplate",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "NotificationTemplate",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "NotificationTemplate",
                type: "character varying(129536)",
                maxLength: 129536,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "NotificationHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(129536)", maxLength: 129536, nullable: false),
                    SendEmailAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ReceiverEmailAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Subject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SenderPhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ReceiverPhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHistory");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "NotificationTemplate",
                newName: "NotificationType");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "NotificationTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "NotificationTemplate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(129536)",
                oldMaxLength: 129536);
        }
    }
}
