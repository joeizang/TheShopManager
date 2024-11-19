using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using ShopManager.DomainModels;

#nullable disable

namespace ShopManager.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlan_Name",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "BillingCycle",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "Price_amount",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "Price_currency",
                table: "SubscriptionPlan");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPlanTypeId",
                table: "SubscriptionPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    BillingCycle = table.Column<BillingCycle>(type: "billing_cycle", nullable: false),
                    Features = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_currency = table.Column<Currency>(type: "currency", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlan_SubscriptionPlanTypeId",
                table: "SubscriptionPlan",
                column: "SubscriptionPlanTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlan_SubscriptionPlanType_SubscriptionPlanTypeId",
                table: "SubscriptionPlan",
                column: "SubscriptionPlanTypeId",
                principalTable: "SubscriptionPlanType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlan_SubscriptionPlanType_SubscriptionPlanTypeId",
                table: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanType");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlan_SubscriptionPlanTypeId",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanTypeId",
                table: "SubscriptionPlan");

            migrationBuilder.AddColumn<BillingCycle>(
                name: "BillingCycle",
                table: "SubscriptionPlan",
                type: "billing_cycle",
                nullable: false,
                defaultValue: BillingCycle.MONTHLY);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubscriptionPlan",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "SubscriptionPlan",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubscriptionPlan",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price_amount",
                table: "SubscriptionPlan",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Currency>(
                name: "Price_currency",
                table: "SubscriptionPlan",
                type: "currency",
                nullable: false,
                defaultValue: Currency.USD);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlan_Name",
                table: "SubscriptionPlan",
                column: "Name",
                unique: true);
        }
    }
}
