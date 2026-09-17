using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_MVC_ITI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoActionAndFakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Employees_EmployeeId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Employees_EmployeeId",
                table: "Payrolls");

            migrationBuilder.CreateTable(
                name: "ApplicationInterviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationProcessId = table.Column<int>(type: "int", nullable: false),
                    InterviewerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationInterviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationInterviews_ApplicationProcesses_ApplicationProcessId",
                        column: x => x.ApplicationProcessId,
                        principalTable: "ApplicationProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationProcessId = table.Column<int>(type: "int", nullable: false),
                    BasicSalaryOffer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationOffers_ApplicationProcesses_ApplicationProcessId",
                        column: x => x.ApplicationProcessId,
                        principalTable: "ApplicationProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruitments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationProcesses_RecruitmentId",
                table: "ApplicationProcesses",
                column: "RecruitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationInterviews_ApplicationProcessId",
                table: "ApplicationInterviews",
                column: "ApplicationProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOffers_ApplicationProcessId",
                table: "ApplicationOffers",
                column: "ApplicationProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationProcesses_Recruitments_RecruitmentId",
                table: "ApplicationProcesses",
                column: "RecruitmentId",
                principalTable: "Recruitments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Employees_EmployeeId",
                table: "Contracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Employees_EmployeeId",
                table: "Payrolls",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationProcesses_Recruitments_RecruitmentId",
                table: "ApplicationProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Employees_EmployeeId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Employees_EmployeeId",
                table: "Payrolls");

            migrationBuilder.DropTable(
                name: "ApplicationInterviews");

            migrationBuilder.DropTable(
                name: "ApplicationOffers");

            migrationBuilder.DropTable(
                name: "Recruitments");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationProcesses_RecruitmentId",
                table: "ApplicationProcesses");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmployeeId",
                table: "Attendances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Employees_EmployeeId",
                table: "Contracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Employees_EmployeeId",
                table: "Payrolls",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
