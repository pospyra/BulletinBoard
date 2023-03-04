using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql_insertSubcategory = $"INSERT INTO public.\"Subcategory\" (\"Id\", \"Name\", \"CategoryId\") " +
            $"VALUES('{Guid.NewGuid()}', 'Уборка' , '5f0f0e0b-e150-4d90-bb51-4186922efc59')," +
           $" ('{Guid.NewGuid()}', 'Курсы, репетитор', '5f0f0e0b-e150-4d90-bb51-4186922efc59')," +
           $" ('{Guid.NewGuid()}', 'Ремонт', '5f0f0e0b-e150-4d90-bb51-4186922efc59')," +
           $" ('{Guid.NewGuid()}', 'Стоительство', '5f0f0e0b-e150-4d90-bb51-4186922efc59')," +
           $"('{Guid.NewGuid()}', 'Реклама', '5f0f0e0b-e150-4d90-bb51-4186922efc59')";

            var sql_insertUser = $"INSERT INTO public.\"User\" (\"Id\", \"Name\", \"Email\", \"Password\" , \"Region\") " +
               $"VALUES('{Guid.NewGuid()}', 'admin', 'pos.pyra@yandex.ru', 'admin', 'Симферополь')";

            migrationBuilder.Sql(sql_insertSubcategory);
            migrationBuilder.Sql(sql_insertUser);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
