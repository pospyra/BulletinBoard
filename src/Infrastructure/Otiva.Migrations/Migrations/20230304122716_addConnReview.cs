using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addConnReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Review_SellerId",
                table: "Review",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_SellerId",
                table: "Review",
                column: "SellerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_SellerId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_SellerId",
                table: "Review");
        }
    }
}
