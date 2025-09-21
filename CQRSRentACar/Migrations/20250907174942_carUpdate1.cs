using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRSRentACar.Migrations
{
    /// <inheritdoc />
    public partial class carUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarImage",
                table: "Cars",
                newName: "CarImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarImageUrl",
                table: "Cars",
                newName: "CarImage");
        }
    }
}
