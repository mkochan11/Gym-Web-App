using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGymReportEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionistsAvailabilities_Receptionists_EmployeeId",
                table: "ReceptionistsAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionistsShifts_Receptionists_EmployeeId",
                table: "ReceptionistsShifts");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "ReceptionistsShifts",
                newName: "ReceptionistId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptionistsShifts_EmployeeId",
                table: "ReceptionistsShifts",
                newName: "IX_ReceptionistsShifts_ReceptionistId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "ReceptionistsAvailabilities",
                newName: "ReceptionistId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptionistsAvailabilities_EmployeeId",
                table: "ReceptionistsAvailabilities",
                newName: "IX_ReceptionistsAvailabilities_ReceptionistId");

            migrationBuilder.CreateTable(
                name: "GymReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewClients = table.Column<int>(type: "int", nullable: true),
                    NewMemberships = table.Column<int>(type: "int", nullable: true),
                    TotalEmployeesCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalIndividualTrainings = table.Column<int>(type: "int", nullable: true),
                    TotalGroupTrainings = table.Column<int>(type: "int", nullable: true),
                    TotalIndividualTrainingsTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    TotalGroupTrainingsTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymReports_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymReports_OwnerId",
                table: "GymReports",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionistsAvailabilities_Receptionists_ReceptionistId",
                table: "ReceptionistsAvailabilities",
                column: "ReceptionistId",
                principalTable: "Receptionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionistsShifts_Receptionists_ReceptionistId",
                table: "ReceptionistsShifts",
                column: "ReceptionistId",
                principalTable: "Receptionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionistsAvailabilities_Receptionists_ReceptionistId",
                table: "ReceptionistsAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionistsShifts_Receptionists_ReceptionistId",
                table: "ReceptionistsShifts");

            migrationBuilder.DropTable(
                name: "GymReports");

            migrationBuilder.RenameColumn(
                name: "ReceptionistId",
                table: "ReceptionistsShifts",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptionistsShifts_ReceptionistId",
                table: "ReceptionistsShifts",
                newName: "IX_ReceptionistsShifts_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "ReceptionistId",
                table: "ReceptionistsAvailabilities",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptionistsAvailabilities_ReceptionistId",
                table: "ReceptionistsAvailabilities",
                newName: "IX_ReceptionistsAvailabilities_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionistsAvailabilities_Receptionists_EmployeeId",
                table: "ReceptionistsAvailabilities",
                column: "EmployeeId",
                principalTable: "Receptionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionistsShifts_Receptionists_EmployeeId",
                table: "ReceptionistsShifts",
                column: "EmployeeId",
                principalTable: "Receptionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
