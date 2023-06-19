using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepApiAutores.Migrations
{
    public partial class relacionmuchosamuchos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoresLibros",
                columns: table => new
                {
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    LibrosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoresLibros", x => new { x.AutorId, x.LibrosId });
                    table.ForeignKey(
                        name: "FK_AutoresLibros_Autors_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoresLibros_Libros_LibrosId",
                        column: x => x.LibrosId,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoresLibros_LibrosId",
                table: "AutoresLibros",
                column: "LibrosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoresLibros");
        }
    }
}
