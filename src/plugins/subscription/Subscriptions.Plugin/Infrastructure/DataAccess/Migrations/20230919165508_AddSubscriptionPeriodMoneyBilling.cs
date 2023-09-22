using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionPeriodMoneyBilling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Billing",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Period_EndDateUtc",
                table: "Subscription",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Period_StartDateUtc",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UnitPrice",
                table: "Subscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Billing",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "Period_EndDateUtc",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "Period_StartDateUtc",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Subscription");
        }
    }
}
