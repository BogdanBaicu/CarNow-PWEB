using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class CarConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Maintenance",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Insurance",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    VIN = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Transmission = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    EngineCC = table.Column<int>(type: "integer", nullable: false),
                    PowerHP = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    FuelType = table.Column<string>(type: "text", nullable: false),
                    BodyType = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.UniqueConstraint("AK_Car_LicensePlate", x => x.LicensePlate);
                    table.UniqueConstraint("AK_Car_VIN", x => x.VIN);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_CarId",
                table: "Maintenance",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_CarId",
                table: "Insurance",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_Car_CarId",
                table: "Insurance",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_Car_CarId",
                table: "Maintenance",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_Car_CarId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_Car_CarId",
                table: "Maintenance");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Maintenance_CarId",
                table: "Maintenance");

            migrationBuilder.DropIndex(
                name: "IX_Insurance_CarId",
                table: "Insurance");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Maintenance");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Insurance");
        }
    }
}
