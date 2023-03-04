using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Seed_DataCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = $"INSERT INTO public.\"Category\" (\"Id\", \"Name\") VALUES('{Guid.NewGuid()}', 'Животные')," +
            $" ('{Guid.NewGuid()}', 'Хобби и отдых')," +
            $" ('{Guid.NewGuid()}', 'Красота и здороье'), " +
            $"('{Guid.NewGuid()}', 'Недвижимость')";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
