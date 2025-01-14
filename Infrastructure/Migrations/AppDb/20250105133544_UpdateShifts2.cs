using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShifts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
