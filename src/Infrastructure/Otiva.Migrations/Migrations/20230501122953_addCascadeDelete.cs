using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoAds_Ad_AdId",
                table: "PhotoAds");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoAds_Ad_AdId",
                table: "PhotoAds",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoAds_Ad_AdId",
                table: "PhotoAds");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoAds_Ad_AdId",
                table: "PhotoAds",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id");
        }
    }
}
