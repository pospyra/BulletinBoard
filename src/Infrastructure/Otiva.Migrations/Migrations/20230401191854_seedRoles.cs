using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var sql = $"INSERT INTO public.\"Roles\" (\"Id\", \"Name\") VALUES('{Guid.NewGuid()}', 'admin')";
            var sql1 = $"INSERT INTO public.\"Roles\" (\"Id\", \"Name\") VALUES('{Guid.NewGuid()}', 'user')";
            migrationBuilder.Sql(sql);
            migrationBuilder.Sql(sql1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
