using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditGymReportEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BudgetReport",
                table: "GymReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ClientsReport",
                table: "GymReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GroupTrainingsReport",
                table: "GymReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IndividualTrainingsReport",
                table: "GymReports",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetReport",
                table: "GymReports");

            migrationBuilder.DropColumn(
                name: "ClientsReport",
                table: "GymReports");

            migrationBuilder.DropColumn(
                name: "GroupTrainingsReport",
                table: "GymReports");

            migrationBuilder.DropColumn(
                name: "IndividualTrainingsReport",
                table: "GymReports");
        }
    }
}
