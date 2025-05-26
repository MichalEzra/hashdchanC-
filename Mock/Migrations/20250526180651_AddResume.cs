using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class AddResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Rezumeh",
                table: "Candidates",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rezumeh",
                table: "Candidates");
        }
    }
}
