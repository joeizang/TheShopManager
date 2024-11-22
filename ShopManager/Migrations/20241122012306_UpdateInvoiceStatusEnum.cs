using Microsoft.EntityFrameworkCore.Migrations;
using ShopManager.DomainModels;

#nullable disable

namespace ShopManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TenantInvoices");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:activation_status", "active,deactivated,inactive,suspended")
                .Annotation("Npgsql:Enum:billing_cycle", "monthly,quarterly,yearly")
                .Annotation("Npgsql:Enum:currency", "aud,cad,cny,eur,gbp,ghs,gmd,gnf,inr,jpy,kes,lrd,mwk,mzn,ngn,rwf,sll,ugx,usd,xaf,xof,xpf,zar,zmw")
                .Annotation("Npgsql:Enum:fairlyused_item_condition", "excellent,fair,good,poor,very_good")
                .Annotation("Npgsql:Enum:invoice_status", "not_payed,overdue,partially_payed,payed")
                .Annotation("Npgsql:Enum:payment_method", "bank_transfer,cash,cheque,crypto_currency,mobile_money,not_set,pos,ussd")
                .Annotation("Npgsql:Enum:payment_status", "failed,pending,successful,uninitialized")
                .OldAnnotation("Npgsql:Enum:activation_status", "active,deactivated,inactive,suspended")
                .OldAnnotation("Npgsql:Enum:billing_cycle", "monthly,quarterly,yearly")
                .OldAnnotation("Npgsql:Enum:currency", "aud,cad,cny,eur,gbp,ghs,gmd,gnf,inr,jpy,kes,lrd,mwk,mzn,ngn,rwf,sll,ugx,usd,xaf,xof,xpf,zar,zmw")
                .OldAnnotation("Npgsql:Enum:fairlyused_item_condition", "excellent,fair,good,poor,very_good")
                .OldAnnotation("Npgsql:Enum:payment_method", "bank_transfer,cash,cheque,crypto_currency,mobile_money,not_set,pos,ussd")
                .OldAnnotation("Npgsql:Enum:payment_status", "failed,pending,successful,uninitialized");

            migrationBuilder.AddColumn<InvoiceStatus>(
                name: "InvoiceStatus",
                table: "TenantInvoices",
                type: "invoice_status",
                nullable: false,
                defaultValue: (InvoiceStatus)10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceStatus",
                table: "TenantInvoices");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:activation_status", "active,deactivated,inactive,suspended")
                .Annotation("Npgsql:Enum:billing_cycle", "monthly,quarterly,yearly")
                .Annotation("Npgsql:Enum:currency", "aud,cad,cny,eur,gbp,ghs,gmd,gnf,inr,jpy,kes,lrd,mwk,mzn,ngn,rwf,sll,ugx,usd,xaf,xof,xpf,zar,zmw")
                .Annotation("Npgsql:Enum:fairlyused_item_condition", "excellent,fair,good,poor,very_good")
                .Annotation("Npgsql:Enum:payment_method", "bank_transfer,cash,cheque,crypto_currency,mobile_money,not_set,pos,ussd")
                .Annotation("Npgsql:Enum:payment_status", "failed,pending,successful,uninitialized")
                .OldAnnotation("Npgsql:Enum:activation_status", "active,deactivated,inactive,suspended")
                .OldAnnotation("Npgsql:Enum:billing_cycle", "monthly,quarterly,yearly")
                .OldAnnotation("Npgsql:Enum:currency", "aud,cad,cny,eur,gbp,ghs,gmd,gnf,inr,jpy,kes,lrd,mwk,mzn,ngn,rwf,sll,ugx,usd,xaf,xof,xpf,zar,zmw")
                .OldAnnotation("Npgsql:Enum:fairlyused_item_condition", "excellent,fair,good,poor,very_good")
                .OldAnnotation("Npgsql:Enum:invoice_status", "not_payed,overdue,partially_payed,payed")
                .OldAnnotation("Npgsql:Enum:payment_method", "bank_transfer,cash,cheque,crypto_currency,mobile_money,not_set,pos,ussd")
                .OldAnnotation("Npgsql:Enum:payment_status", "failed,pending,successful,uninitialized");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TenantInvoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
