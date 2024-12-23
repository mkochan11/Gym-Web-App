using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShiftsAvailabilitiesLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTrainersAvailabilities");

            migrationBuilder.DropTable(
                name: "GroupTrainersShifts");

            migrationBuilder.DropTable(
                name: "PersonalTrainerAvailabilities");

            migrationBuilder.DropTable(
                name: "PersonalTrainersShifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTrainersAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrainersAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTrainersAvailabilities_GroupTrainers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "GroupTrainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTrainersShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrainersShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTrainersShifts_GroupTrainers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "GroupTrainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalTrainerAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTrainerAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalTrainerAvailabilities_PersonalTrainers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "PersonalTrainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalTrainersShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTrainersShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalTrainersShifts_PersonalTrainers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "PersonalTrainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTrainersAvailabilities_EmployeeId",
                table: "GroupTrainersAvailabilities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTrainersShifts_EmployeeId",
                table: "GroupTrainersShifts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainerAvailabilities_EmployeeId",
                table: "PersonalTrainerAvailabilities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainersShifts_EmployeeId",
                table: "PersonalTrainersShifts",
                column: "EmployeeId");
        }
    }
}
