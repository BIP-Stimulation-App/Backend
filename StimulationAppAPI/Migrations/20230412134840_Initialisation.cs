using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StimulationAppAPI.Migrations
{
    public partial class Initialisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(30)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(30)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Role = table.Column<string>(type: "varchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(30)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_Login_User_UserName",
                        column: x => x.UserName,
                        principalTable: "User",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserName", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { "Joske", "Joske.Peeters@gmail.com", "Joske", "Peeters", "User" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserName", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { "Nicolas", "nic.vanruy@gmail.com", "Nicolas", "Vanruysseveldt", "Admin" });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "UserName", "Password" },
                values: new object[] { "Joske", "Test123" });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "UserName", "Password" },
                values: new object[] { "Nicolas", "Test123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
