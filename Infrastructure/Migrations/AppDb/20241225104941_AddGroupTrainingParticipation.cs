using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTrainingParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientGroupTraining");

            migrationBuilder.CreateTable(
                name: "GroupTrainingParticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupTrainingId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrainingParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTrainingParticipation_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTrainingParticipation_GroupTrainings_GroupTrainingId",
                        column: x => x.GroupTrainingId,
                        principalTable: "GroupTrainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTrainingParticipation_ClientId",
                table: "GroupTrainingParticipation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTrainingParticipation_GroupTrainingId",
                table: "GroupTrainingParticipation",
                column: "GroupTrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTrainingParticipation");

            migrationBuilder.CreateTable(
                name: "ClientGroupTraining",
                columns: table => new
                {
                    GroupTrainingsId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGroupTraining", x => new { x.GroupTrainingsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ClientGroupTraining_Clients_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientGroupTraining_GroupTrainings_GroupTrainingsId",
                        column: x => x.GroupTrainingsId,
                        principalTable: "GroupTrainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroupTraining_ParticipantsId",
                table: "ClientGroupTraining",
                column: "ParticipantsId");
        }
    }
}
