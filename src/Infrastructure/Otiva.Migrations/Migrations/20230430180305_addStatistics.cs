using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StatisticsTableAdsId",
                table: "Ad",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatisticsTableAds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdId = table.Column<Guid>(type: "uuid", nullable: true),
                    QuantityView = table.Column<int>(type: "integer", nullable: false),
                    QuantityAddToFavorites = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsTableAds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_StatisticsTableAdsId",
                table: "Ad",
                column: "StatisticsTableAdsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad",
                column: "StatisticsTableAdsId",
                principalTable: "StatisticsTableAds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad");

            migrationBuilder.DropTable(
                name: "StatisticsTableAds");

            migrationBuilder.DropIndex(
                name: "IX_Ad_StatisticsTableAdsId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "StatisticsTableAdsId",
                table: "Ad");
        }
    }
}
