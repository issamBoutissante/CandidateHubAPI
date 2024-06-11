using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateEmailUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_Email",
                table: "Candidates");
        }
    }
}
