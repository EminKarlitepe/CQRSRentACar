using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRSRentACar.Migrations
{
    /// <inheritdoc />
    public partial class AirportInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DropOffAirportId",
                table: "CarRentals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PickUpAirportId",
                table: "CarRentals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryIso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_DropOffAirportId",
                table: "CarRentals",
                column: "DropOffAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_PickUpAirportId",
                table: "CarRentals",
                column: "PickUpAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Airports_DropOffAirportId",
                table: "CarRentals",
                column: "DropOffAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Airports_PickUpAirportId",
                table: "CarRentals",
                column: "PickUpAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Airports_DropOffAirportId",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Airports_PickUpAirportId",
                table: "CarRentals");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_DropOffAirportId",
                table: "CarRentals");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_PickUpAirportId",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "DropOffAirportId",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "PickUpAirportId",
                table: "CarRentals");
        }
    }
}
