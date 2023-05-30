using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StimulationAppAPI.Migrations
{
    public partial class privacy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_User_UserName",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_User_UserName",
                table: "Medication");

            migrationBuilder.AddColumn<bool>(
                name: "Anonymous",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Login_User_UserName",
                table: "Login",
                column: "UserName",
                principalTable: "User",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_User_UserName",
                table: "Medication",
                column: "UserName",
                principalTable: "User",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_User_UserName",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_User_UserName",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "Anonymous",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_User_UserName",
                table: "Login",
                column: "UserName",
                principalTable: "User",
                principalColumn: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_User_UserName",
                table: "Medication",
                column: "UserName",
                principalTable: "User",
                principalColumn: "UserName");
        }
    }
}
