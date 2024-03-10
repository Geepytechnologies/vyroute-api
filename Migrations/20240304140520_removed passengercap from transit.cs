using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyroute.Migrations
{
    /// <inheritdoc />
    public partial class removedpassengercapfromtransit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiclePassengerCap",
                table: "Transits");

            migrationBuilder.AddColumn<int>(
                name: "VehiclePassengerCap",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiclePassengerCap",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehiclePassengerCap",
                table: "Transits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
