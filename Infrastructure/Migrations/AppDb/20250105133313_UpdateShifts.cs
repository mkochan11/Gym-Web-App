using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptionistsAvailabilities");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "ReceptionistsShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistsShifts_ManagerId",
                table: "ReceptionistsShifts",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionistsShifts_Managers_ManagerId",
                table: "ReceptionistsShifts",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionistsShifts_Managers_ManagerId",
                table: "ReceptionistsShifts");

            migrationBuilder.DropIndex(
                name: "IX_ReceptionistsShifts_ManagerId",
                table: "ReceptionistsShifts");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "ReceptionistsShifts");

            migrationBuilder.CreateTable(
                name: "ReceptionistsAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionistsAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionistsAvailabilities_Receptionists_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalTable: "Receptionists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistsAvailabilities_ReceptionistId",
                table: "ReceptionistsAvailabilities",
                column: "ReceptionistId");
        }
    }
}
