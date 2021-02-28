using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherData2._0.Migrations
{
    public partial class InitDayAvrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayAvrs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndoorTemperature = table.Column<float>(type: "real", nullable: false),
                    OutdoorTemperature = table.Column<float>(type: "real", nullable: false),
                    IndoorHumidity = table.Column<float>(type: "real", nullable: false),
                    OutdoorHumidity = table.Column<float>(type: "real", nullable: false),
                    IndoorMold = table.Column<int>(type: "int", nullable: false),
                    OutdoorMold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayAvrs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayAvrs");
        }
    }
}
