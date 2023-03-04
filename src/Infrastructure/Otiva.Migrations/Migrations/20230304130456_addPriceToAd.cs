using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addPriceToAd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Ad",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Ad",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Ad");
        }
    }
}
