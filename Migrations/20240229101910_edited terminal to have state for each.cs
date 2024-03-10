using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyroute.Migrations
{
    /// <inheritdoc />
    public partial class editedterminaltohavestateforeach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Terminals",
                newName: "DepartureState");

            migrationBuilder.AddColumn<string>(
                name: "ArrivalState",
                table: "Terminals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalState",
                table: "Terminals");

            migrationBuilder.RenameColumn(
                name: "DepartureState",
                table: "Terminals",
                newName: "State");
        }
    }
}
