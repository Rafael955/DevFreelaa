using Microsoft.EntityFrameworkCore.Migrations;

namespace DevFreela.Infrastructure.Migrations
{
    public partial class CorrecaoModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersLogin_UserLoginId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UsersLogin");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserLoginId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserLoginId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserLoginId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isLogged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLoginId",
                table: "Users",
                column: "UserLoginId",
                unique: true,
                filter: "[UserLoginId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersLogin_UserLoginId",
                table: "Users",
                column: "UserLoginId",
                principalTable: "UsersLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
