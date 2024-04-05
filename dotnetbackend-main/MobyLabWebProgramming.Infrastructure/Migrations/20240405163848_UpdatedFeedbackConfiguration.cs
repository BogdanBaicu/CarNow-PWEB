using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class UpdatedFeedbackConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Car_CarId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_User_EmployeeId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_CarId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_EmployeeId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Feedback");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Feedback",
                newName: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ReservationId",
                table: "Feedback",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Reservation_ReservationId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_ReservationId",
                table: "Feedback");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Feedback",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Feedback",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CarId",
                table: "Feedback",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_EmployeeId",
                table: "Feedback",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Car_CarId",
                table: "Feedback",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_User_EmployeeId",
                table: "Feedback",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
