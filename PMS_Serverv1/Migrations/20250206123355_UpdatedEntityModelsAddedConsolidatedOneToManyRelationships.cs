using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS_Serverv1.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntityModelsAddedConsolidatedOneToManyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Status_StatusId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_StatusId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Name_Description_DateCompleted_Deadline",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateStarted",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name_Description_Deadline",
                table: "Projects",
                columns: new[] { "Name", "Description", "Deadline" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_Name_Description_Deadline",
                table: "Projects");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStarted",
                table: "Task",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Task",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "Projects",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_StatusId",
                table: "Task",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name_Description_DateCompleted_Deadline",
                table: "Projects",
                columns: new[] { "Name", "Description", "DateCompleted", "Deadline" });

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Status_StatusId",
                table: "Task",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }
    }
}
