using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

#nullable disable

namespace ShopManager.Migrations
{
    /// <inheritdoc />
    public partial class TenantAndSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Tenant_TenantId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "TenantInvoice");

            migrationBuilder.DropTable(
                name: "TenantPayment");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanType");

            migrationBuilder.DropTable(
                name: "TenantPaymentMethod");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Suppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Shops",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SaleItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Inventories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FairlyUsedItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanTypes",
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
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContactName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BillingAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ActivationStatus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PaymentStatus = table.Column<PaymentStatus>(type: "payment_status", nullable: false, defaultValue: PaymentStatus.UNINITIALIZED),
                    NextBillingDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    SubscriptionStartDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    SubscriptionEndDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<ActivationStatus>(type: "activation_status", nullable: false, defaultValue: ActivationStatus.INACTIVE),
                    SubscriptionPlanTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlans_SubscriptionPlanTypes_SubscriptionPlanTyp~",
                        column: x => x.SubscriptionPlanTypeId,
                        principalTable: "SubscriptionPlanTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlans_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DueDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    InvoiceReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountDue_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountDue_currency = table.Column<Currency>(type: "currency", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantInvoices_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentDetails = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IsDefaultPaymentMethod = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPaymentMethods_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<PaymentStatus>(type: "payment_status", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountPaid_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountPaid_currency = table.Column<Currency>(type: "currency", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPayments_TenantPaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "TenantPaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TenantPayments_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_SubscriptionPlanTypeId",
                table: "SubscriptionPlans",
                column: "SubscriptionPlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_TenantId",
                table: "SubscriptionPlans",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvoices_DueDate",
                table: "TenantInvoices",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvoices_InvoiceReference",
                table: "TenantInvoices",
                column: "InvoiceReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvoices_TenantId",
                table: "TenantInvoices",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPaymentMethods_TenantId",
                table: "TenantPaymentMethods",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayments_PaymentDate",
                table: "TenantPayments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayments_PaymentMethodId",
                table: "TenantPayments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayments_PaymentReference",
                table: "TenantPayments",
                column: "PaymentReference");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayments_TenantId",
                table: "TenantPayments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_EmailAddress",
                table: "Tenants",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Tenants_TenantId",
                table: "Shops",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Tenants_TenantId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "TenantInvoices");

            migrationBuilder.DropTable(
                name: "TenantPayments");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanTypes");

            migrationBuilder.DropTable(
                name: "TenantPaymentMethods");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FairlyUsedItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillingCycle = table.Column<BillingCycle>(type: "billing_cycle", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    Features = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_currency = table.Column<Currency>(type: "currency", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivationStatus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BillingAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ContactName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NextBillingDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentStatus = table.Column<PaymentStatus>(type: "payment_status", nullable: false, defaultValue: PaymentStatus.UNINITIALIZED),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SubscriptionEndDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    SubscriptionStartDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionPlanTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<ActivationStatus>(type: "activation_status", nullable: false, defaultValue: ActivationStatus.INACTIVE),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlan_SubscriptionPlanType_SubscriptionPlanTypeId",
                        column: x => x.SubscriptionPlanTypeId,
                        principalTable: "SubscriptionPlanType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlan_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DueDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    InvoiceReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    AmountDue_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountDue_currency = table.Column<Currency>(type: "currency", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantInvoice_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantPaymentMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    IsDefaultPaymentMethod = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentDetails = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPaymentMethod_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenantPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PaymentDate = table.Column<ZonedDateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<PaymentStatus>(type: "payment_status", nullable: false),
                    UpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    AmountPaid_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountPaid_currency = table.Column<Currency>(type: "currency", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPayment_TenantPaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "TenantPaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TenantPayment_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlan_SubscriptionPlanTypeId",
                table: "SubscriptionPlan",
                column: "SubscriptionPlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlan_TenantId",
                table: "SubscriptionPlan",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_EmailAddress",
                table: "Tenant",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvoice_DueDate",
                table: "TenantInvoice",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvoice_InvoiceReference",
                table: "TenantInvoice",
                column: "InvoiceReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvoice_TenantId",
                table: "TenantInvoice",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayment_PaymentDate",
                table: "TenantPayment",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayment_PaymentMethodId",
                table: "TenantPayment",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayment_PaymentReference",
                table: "TenantPayment",
                column: "PaymentReference");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPayment_TenantId",
                table: "TenantPayment",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPaymentMethod_TenantId",
                table: "TenantPaymentMethod",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Tenant_TenantId",
                table: "Shops",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");
        }
    }
}
