﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class seedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var sql = $"INSERT INTO public.\"Roles\" (\"Id\", \"Name\") VALUES('{Guid.NewGuid()}', 'Admin', 'ADMIN')";
            var sql1 = $"INSERT INTO public.\"Roles\" (\"Id\", \"Name\") VALUES('{Guid.NewGuid()}', 'User', 'USER')";
            migrationBuilder.Sql(sql);
            migrationBuilder.Sql(sql1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
