using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC23._10._1403.Migrations
{
    /// <inheritdoc />
    public partial class thirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "_categories",
                columns: new[] { "Id", "Name", "Order" },
                values: new object[] { 1, "Ali", "100" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "_categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
