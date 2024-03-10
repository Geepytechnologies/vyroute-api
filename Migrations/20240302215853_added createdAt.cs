using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyroute.Migrations
{
    /// <inheritdoc />
    public partial class addedcreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TerminalId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Transits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TerminalId",
                table: "Vehicles",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Terminals_TerminalId",
                table: "Vehicles",
                column: "TerminalId",
                principalTable: "Terminals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Terminals_TerminalId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TerminalId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Transits");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");
        }
    }
}
