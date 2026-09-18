using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_MVC_ITI.Migrations
{
    /// <inheritdoc />
    public partial class AddInterviewToApplicationOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterviewId",
                table: "ApplicationOffers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOffers_InterviewId",
                table: "ApplicationOffers",
                column: "InterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationOffers_ApplicationInterviews_InterviewId",
                table: "ApplicationOffers",
                column: "InterviewId",
                principalTable: "ApplicationInterviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationOffers_ApplicationInterviews_InterviewId",
                table: "ApplicationOffers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationOffers_InterviewId",
                table: "ApplicationOffers");

            migrationBuilder.DropColumn(
                name: "InterviewId",
                table: "ApplicationOffers");
        }
    }
}
