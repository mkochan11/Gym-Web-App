using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipationDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTrainingParticipation_Clients_ClientId",
                table: "GroupTrainingParticipation");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTrainingParticipation_GroupTrainings_GroupTrainingId",
                table: "GroupTrainingParticipation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTrainingParticipation",
                table: "GroupTrainingParticipation");

            migrationBuilder.RenameTable(
                name: "GroupTrainingParticipation",
                newName: "GroupTrainingParticipations");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTrainingParticipation_GroupTrainingId",
                table: "GroupTrainingParticipations",
                newName: "IX_GroupTrainingParticipations_GroupTrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTrainingParticipation_ClientId",
                table: "GroupTrainingParticipations",
                newName: "IX_GroupTrainingParticipations_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTrainingParticipations",
                table: "GroupTrainingParticipations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTrainingParticipations_Clients_ClientId",
                table: "GroupTrainingParticipations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTrainingParticipations_GroupTrainings_GroupTrainingId",
                table: "GroupTrainingParticipations",
                column: "GroupTrainingId",
                principalTable: "GroupTrainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTrainingParticipations_Clients_ClientId",
                table: "GroupTrainingParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTrainingParticipations_GroupTrainings_GroupTrainingId",
                table: "GroupTrainingParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTrainingParticipations",
                table: "GroupTrainingParticipations");

            migrationBuilder.RenameTable(
                name: "GroupTrainingParticipations",
                newName: "GroupTrainingParticipation");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTrainingParticipations_GroupTrainingId",
                table: "GroupTrainingParticipation",
                newName: "IX_GroupTrainingParticipation_GroupTrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTrainingParticipations_ClientId",
                table: "GroupTrainingParticipation",
                newName: "IX_GroupTrainingParticipation_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTrainingParticipation",
                table: "GroupTrainingParticipation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTrainingParticipation_Clients_ClientId",
                table: "GroupTrainingParticipation",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTrainingParticipation_GroupTrainings_GroupTrainingId",
                table: "GroupTrainingParticipation",
                column: "GroupTrainingId",
                principalTable: "GroupTrainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
