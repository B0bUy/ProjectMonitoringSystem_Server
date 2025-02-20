using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS_Serverv1.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTableNamefromtasktotasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Departments_DepartmentId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Projects_ProjectId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Task_ParentTaskId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Task_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_ParentTaskId",
                table: "Tasks",
                newName: "IX_Tasks_ParentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_Name_Description_Deadline",
                table: "Tasks",
                newName: "IX_Tasks_Name_Description_Deadline");

            migrationBuilder.RenameIndex(
                name: "IX_Task_DepartmentId",
                table: "Tasks",
                newName: "IX_Tasks_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Departments_DepartmentId",
                table: "Tasks",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_ParentTaskId",
                table: "Tasks",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Departments_DepartmentId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_ParentTaskId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "Task",
                newName: "IX_Task_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ParentTaskId",
                table: "Task",
                newName: "IX_Task_ParentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Name_Description_Deadline",
                table: "Task",
                newName: "IX_Task_Name_Description_Deadline");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_DepartmentId",
                table: "Task",
                newName: "IX_Task_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Departments_DepartmentId",
                table: "Task",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Projects_ProjectId",
                table: "Task",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Task_ParentTaskId",
                table: "Task",
                column: "ParentTaskId",
                principalTable: "Task",
                principalColumn: "TaskId");
        }
    }
}
