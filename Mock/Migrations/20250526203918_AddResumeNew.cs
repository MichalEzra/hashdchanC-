using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class AddResumeNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RezumehName",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RezumehName",
                table: "Candidates");
        }
    }
}
