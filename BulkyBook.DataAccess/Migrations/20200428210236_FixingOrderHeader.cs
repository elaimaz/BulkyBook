using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class FixingOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "orderTotal",
                table: "OrderHeaders",
                newName: "OrderTotal");

            migrationBuilder.RenameColumn(
                name: "orderDate",
                table: "OrderHeaders",
                newName: "OrderDate");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicarionUserId",
                table: "OrderHeaders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderTotal",
                table: "OrderHeaders",
                newName: "orderTotal");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "OrderHeaders",
                newName: "orderDate");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicarionUserId",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
