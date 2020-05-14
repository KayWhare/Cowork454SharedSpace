using Microsoft.EntityFrameworkCore.Migrations;

namespace CoWork454.Migrations
{
    public partial class removeditem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Item_ItemId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Product_ItemId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ItemId",
                table: "Product",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Item_ItemId",
                table: "Product",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
