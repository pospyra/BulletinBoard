using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeNull1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad");

            migrationBuilder.AlterColumn<Guid>(
                name: "StatisticsTableAdsId",
                table: "Ad",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad",
                column: "StatisticsTableAdsId",
                principalTable: "StatisticsTableAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad");

            migrationBuilder.AlterColumn<Guid>(
                name: "StatisticsTableAdsId",
                table: "Ad",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_StatisticsTableAds_StatisticsTableAdsId",
                table: "Ad",
                column: "StatisticsTableAdsId",
                principalTable: "StatisticsTableAds",
                principalColumn: "Id");
        }
    }
}
