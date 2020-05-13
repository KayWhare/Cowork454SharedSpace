using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class modelupdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Product",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date_start",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date_end",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Date_start",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<string>(
                name: "Date_end",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
