using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ProductManager.Infrastructure.Data;

#nullable disable

namespace ProductManager.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260904000000_AddMissingMigration")]
    public partial class AddMissingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // No schema changes. Esta migración sirve para sincronizar el snapshot con el modelo actual.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No rollback necesario.
        }
    }
}
