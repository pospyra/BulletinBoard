using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class changeCongStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad");

            migrationBuilder.DropIndex(
                name: "IX_Ad_StatisticsTableAdsId",
                table: "Ad");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsTableAds_AdId",
                table: "StatisticsTableAds",
                column: "AdId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticsTableAds_Ad_AdId",
                table: "StatisticsTableAds",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatisticsTableAds_Ad_AdId",
                table: "StatisticsTableAds");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsTableAds_AdId",
                table: "StatisticsTableAds");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_StatisticsTableAdsId",
                table: "Ad",
                column: "StatisticsTableAdsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad",
                column: "StatisticsTableAdsId",
                principalTable: "StatisticsTableAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
