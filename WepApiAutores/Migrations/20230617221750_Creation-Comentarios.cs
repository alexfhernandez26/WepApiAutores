using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepApiAutores.Migrations
{
    public partial class CreationComentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<int>(type: "int", nullable: false),
                    libroId = table.Column<int>(type: "int", nullable: false),
                    LibrosId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comentarios_Libros_LibrosId",
                        column: x => x.LibrosId,
                        principalTable: "Libros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_LibrosId",
                table: "Comentarios",
                column: "LibrosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");
        }
    }
}
