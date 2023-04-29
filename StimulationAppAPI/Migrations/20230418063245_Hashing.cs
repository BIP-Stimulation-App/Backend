using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StimulationAppAPI.Migrations
{
    public partial class Hashing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Login",
                keyColumn: "UserName",
                keyValue: "Joske");

            migrationBuilder.DeleteData(
                table: "Login",
                keyColumn: "UserName",
                keyValue: "Nicolas");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserName",
                keyValue: "Joske");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserName",
                keyValue: "Nicolas");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Login",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.CreateTable(
                name: "Salts",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(30)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salts", x => x.UserName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Login",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "UserName", "Password" },
                values: new object[,]
                {
                    { "Joske", "Test123" },
                    { "Nicolas", "Test123" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserName", "Email", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { "Joske", "Joske.Peeters@gmail.com", "Joske", "Peeters", "User" },
                    { "Nicolas", "nic.vanruy@gmail.com", "Nicolas", "Vanruysseveldt", "Admin" }
                });
        }
    }
}
