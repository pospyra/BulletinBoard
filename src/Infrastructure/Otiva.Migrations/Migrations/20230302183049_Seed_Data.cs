using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = $"INSERT INTO public.\"Category\" (\"Id\", \"Name\") VALUES('{Guid.NewGuid()}', 'Авто')," +
                $" ('{Guid.NewGuid()}', 'Услуги') ,('{Guid.NewGuid()}', 'Авто') ";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
