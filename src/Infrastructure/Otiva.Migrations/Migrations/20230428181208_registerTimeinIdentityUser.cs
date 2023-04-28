using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class registerTimeinIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KodBase64",
                table: "DomainUser");

            migrationBuilder.RenameColumn(
                name: "RegistrationDateTime",
                table: "DomainUser",
                newName: "DateRegistration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateRegistration",
                table: "DomainUser",
                newName: "RegistrationDateTime");

            migrationBuilder.AddColumn<string>(
                name: "KodBase64",
                table: "DomainUser",
                type: "text",
                nullable: true);
        }
    }
}
