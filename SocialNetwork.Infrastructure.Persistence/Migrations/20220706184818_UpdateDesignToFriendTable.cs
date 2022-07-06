using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Infrastructure.Persistence.Migrations
{
    public partial class UpdateDesignToFriendTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Friends_FriendId",
                table: "Publications");

            migrationBuilder.AlterColumn<int>(
                name: "FriendId",
                table: "Publications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Friends_FriendId",
                table: "Publications",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Friends_FriendId",
                table: "Publications");

            migrationBuilder.AlterColumn<int>(
                name: "FriendId",
                table: "Publications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Friends_FriendId",
                table: "Publications",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
