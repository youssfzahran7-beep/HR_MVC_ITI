using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_MVC_ITI.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationProcessRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicationProcesses_CandidateId",
                table: "ApplicationProcesses",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationProcesses_Candidates_CandidateId",
                table: "ApplicationProcesses",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationProcesses_Candidates_CandidateId",
                table: "ApplicationProcesses");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationProcesses_CandidateId",
                table: "ApplicationProcesses");
        }
    }
}
