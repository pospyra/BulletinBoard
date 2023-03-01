using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddSubcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Ad",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_CategoryId",
                table: "Ad",
                newName: "IX_Ad_SubcategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Ad",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Subcategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_CategoryId",
                table: "Subcategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Subcategory_SubcategoryId",
                table: "Ad",
                column: "SubcategoryId",
                principalTable: "Subcategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Subcategory_SubcategoryId",
                table: "Ad");

            migrationBuilder.DropTable(
                name: "Subcategory");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Ad");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "Ad",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_SubcategoryId",
                table: "Ad",
                newName: "IX_Ad_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
