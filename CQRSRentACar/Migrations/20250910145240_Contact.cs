using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRSRentACar.Migrations
{
    /// <inheritdoc />
    public partial class Contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Airports_DropOffAirportId",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Airports_PickUpAirportId",
                table: "CarRentals");

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    ContactMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    AiResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.ContactMessageId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Airports_DropOffAirportId",
                table: "CarRentals",
                column: "DropOffAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Airports_PickUpAirportId",
                table: "CarRentals",
                column: "PickUpAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);
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
                name: "ContactMessages");

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
    }
}
