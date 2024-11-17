using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShopProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CacRegistrationNumber",
                table: "Shops",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxIdentificationNUmber",
                table: "Shops",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CacRegistrationNumber",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "TaxIdentificationNUmber",
                table: "Shops");
        }
    }
}
