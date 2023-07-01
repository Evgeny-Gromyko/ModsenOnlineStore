using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModsenOnlineStore.Login.API.Migrations
{
    /// <inheritdoc />
    public partial class MoneyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "Users");
        }
    }
}
