using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class addNewTableMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCandidateGirl = table.Column<int>(type: "int", nullable: false),
                    IdCandidateGuy = table.Column<int>(type: "int", nullable: false),
                    IdMatchmaker = table.Column<int>(type: "int", nullable: false),
                    IsEngaged = table.Column<bool>(type: "bit", nullable: false),
                    DateMatch = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmationGirl = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmationGuy = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Candidates_IdCandidateGirl",
                        column: x => x.IdCandidateGirl,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Candidates_IdCandidateGuy",
                        column: x => x.IdCandidateGuy,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Matchmakers_IdMatchmaker",
                        column: x => x.IdMatchmaker,
                        principalTable: "Matchmakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdCandidateGirl",
                table: "Matches",
                column: "IdCandidateGirl");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdCandidateGuy",
                table: "Matches",
                column: "IdCandidateGuy");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdMatchmaker",
                table: "Matches",
                column: "IdMatchmaker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
