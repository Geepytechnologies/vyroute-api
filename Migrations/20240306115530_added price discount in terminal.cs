using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyroute.Migrations
{
    /// <inheritdoc />
    public partial class addedpricediscountinterminal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Terminals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Pricediscountpercent",
                table: "Terminals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pricediscountpercent",
                table: "Terminals");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Terminals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
