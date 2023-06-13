using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class delCascadeUserAd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_DomainUser_DomainUserId",
                table: "Ad");

            migrationBuilder.AlterColumn<Guid>(
                name: "DomainUserId",
                table: "Ad",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_DomainUser_DomainUserId",
                table: "Ad",
                column: "DomainUserId",
                principalTable: "DomainUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_DomainUser_DomainUserId",
                table: "Ad");

            migrationBuilder.AlterColumn<Guid>(
                name: "DomainUserId",
                table: "Ad",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_DomainUser_DomainUserId",
                table: "Ad",
                column: "DomainUserId",
                principalTable: "DomainUser",
                principalColumn: "Id");
        }
    }
}
