using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Infrastructure.Persistence.Migrations
{
    public partial class AddSomePropertiesToFriendTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoUrl",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhotoUrl",
                table: "Friends");
        }
    }
}