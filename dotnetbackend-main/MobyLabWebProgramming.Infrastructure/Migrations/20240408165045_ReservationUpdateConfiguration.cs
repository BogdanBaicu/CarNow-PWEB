using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class ReservationUpdateConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Car_CarId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_User_CustomerId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Car_CarId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CustomerId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_EmployeeId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CarId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Reservation",
                newName: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RequestId",
                table: "Reservation",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Car_CarId",
                table: "Request",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_User_CustomerId",
                table: "Request",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Request_RequestId",
                table: "Reservation",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_EmployeeId",
                table: "Reservation",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Car_CarId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_User_CustomerId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Request_RequestId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_EmployeeId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RequestId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Reservation",
                newName: "CustomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Reservation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Reservation",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Reservation",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Reservation",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CarId",
                table: "Reservation",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Car_CarId",
                table: "Request",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_User_CustomerId",
                table: "Request",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Car_CarId",
                table: "Reservation",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_CustomerId",
                table: "Reservation",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_EmployeeId",
                table: "Reservation",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
