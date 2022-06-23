using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Persistence.Migrations
{
    public partial class Added_PostName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Postname",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postname",
                table: "Posts");
        }
    }
}
