using Microsoft.EntityFrameworkCore.Migrations;

namespace DevFreela.Infrastructure.Migrations
{
    public partial class CorrecaoUserLoginId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserLoginId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserLoginId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLoginId",
                table: "Users",
                column: "UserLoginId",
                unique: true,
                filter: "[UserLoginId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserLoginId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserLoginId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLoginId",
                table: "Users",
                column: "UserLoginId",
                unique: true);
        }
    }
}
