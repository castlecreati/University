using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApiBackend.Migrations
{
    public partial class establecerelacionUserestudiante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Users_UserId",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_UserId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Estudiantes");
        }
    }
}
