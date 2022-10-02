using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApiBackend.Migrations
{
    public partial class jsonIgnoreEnRelacionEstudianteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Users_UserId",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_UserId",
                table: "Estudiantes");

            migrationBuilder.AddColumn<int>(
                name: "FKUserId",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_FKUserId",
                table: "Estudiantes",
                column: "FKUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Users_FKUserId",
                table: "Estudiantes",
                column: "FKUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Users_FKUserId",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_FKUserId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "FKUserId",
                table: "Estudiantes");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_UserId",
                table: "Estudiantes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Users_UserId",
                table: "Estudiantes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
