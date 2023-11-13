﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notifications.Infrastructure.Persistence.DataContexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notifications.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20231113152116_AddNotificationTemplate")]
    partial class AddNotificationTemplate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entities.NotificationTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NotificationType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("NotificationTemplate");

                    b.HasDiscriminator<int>("NotificationType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entities.EmailTemplate", b =>
                {
                    b.HasBaseType("Notifications.Infrastructure.Domain.Entities.NotificationTemplate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Notifications.Infrastructure.Domain.Entities.SmsTemplate", b =>
                {
                    b.HasBaseType("Notifications.Infrastructure.Domain.Entities.NotificationTemplate");

                    b.HasDiscriminator().HasValue(1);
                });
#pragma warning restore 612, 618
        }
    }
}
