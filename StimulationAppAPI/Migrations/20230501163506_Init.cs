using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StimulationAppAPI.Migrations
{
    public partial class Init : Migration
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
                    Password = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_Login_User_UserName",
                        column: x => x.UserName,
                        principalTable: "User",
                        principalColumn: "UserName");
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Time = table.Column<DateTime>(type: "DATE", nullable: false),
                    Frequency = table.Column<short>(type: "smallint", nullable: false),
                    UserName = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_User_UserName",
                        column: x => x.UserName,
                        principalTable: "User",
                        principalColumn: "UserName");
                });

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(30)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_PasswordResets_Login_UserName",
                        column: x => x.UserName,
                        principalTable: "Login",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salts",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(30)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salts", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_Salts_Login_UserName",
                        column: x => x.UserName,
                        principalTable: "Login",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medication_UserName",
                table: "Medication",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropTable(
                name: "Salts");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
