using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class BlogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
