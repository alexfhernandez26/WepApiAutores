using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepApiAutores.Migrations
{
    public partial class loqueseadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Libros_LibrosId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "libroId",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "LibrosId",
                table: "Comentarios",
                newName: "librosId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_LibrosId",
                table: "Comentarios",
                newName: "IX_Comentarios_librosId");

            migrationBuilder.AlterColumn<int>(
                name: "librosId",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Libros_librosId",
                table: "Comentarios",
                column: "librosId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Libros_librosId",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "librosId",
                table: "Comentarios",
                newName: "LibrosId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_librosId",
                table: "Comentarios",
                newName: "IX_Comentarios_LibrosId");

            migrationBuilder.AlterColumn<int>(
                name: "LibrosId",
                table: "Comentarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "libroId",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Libros_LibrosId",
                table: "Comentarios",
                column: "LibrosId",
                principalTable: "Libros",
                principalColumn: "Id");
        }
    }
}
