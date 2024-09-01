using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationMvc.Migrations
{
    public partial class winformklj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ReservationServices");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ReservationServices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ReservationServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ReservationServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
