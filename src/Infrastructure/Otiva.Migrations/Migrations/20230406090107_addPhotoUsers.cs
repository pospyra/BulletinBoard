using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addPhotoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBirthday",
                table: "DomainUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Ad",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PhotoAds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KodBase64 = table.Column<string>(type: "text", nullable: true),
                    AdId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoAds_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhotoUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KodBase64 = table.Column<string>(type: "text", nullable: true),
                    DomainUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoUsers_DomainUser_DomainUserId",
                        column: x => x.DomainUserId,
                        principalTable: "DomainUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoAds_AdId",
                table: "PhotoAds",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUsers_DomainUserId",
                table: "PhotoUsers",
                column: "DomainUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoAds");

            migrationBuilder.DropTable(
                name: "PhotoUsers");

            migrationBuilder.DropColumn(
                name: "DateBirthday",
                table: "DomainUser");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Ad");

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdId = table.Column<Guid>(type: "uuid", nullable: true),
                    KodBase64 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AdId",
                table: "Photo",
                column: "AdId");
        }
    }
}
