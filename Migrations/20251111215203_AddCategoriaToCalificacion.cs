using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaToCalificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Calificaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Categoria", table: "Calificaciones");
        }
    }
}
