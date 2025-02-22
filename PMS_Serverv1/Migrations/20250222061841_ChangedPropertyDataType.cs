using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS_Serverv1.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropertyDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInclusions_Clients_ClientId",
                table: "ClientInclusions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInclusions_Projects_ProjectId",
                table: "ClientInclusions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ClientInclusions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ClientInclusions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("14772961-c4fc-4dcd-9a78-ef20ad55087f"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 14, 18, 39, 864, DateTimeKind.Local).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("37426077-ccdf-4043-b7f1-5aef998f1a45"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 14, 18, 39, 864, DateTimeKind.Local).AddTicks(9934));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("6141b753-bfc8-4af2-8912-4b2828459c2b"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 14, 18, 39, 864, DateTimeKind.Local).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("e0684bf4-d91b-4099-ac68-0bdbb9bc0a50"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 14, 18, 39, 864, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("eb6afbf8-63b0-46bd-bb21-ffec9f890fec"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 14, 18, 39, 864, DateTimeKind.Local).AddTicks(9926));

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInclusions_Clients_ClientId",
                table: "ClientInclusions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInclusions_Projects_ProjectId",
                table: "ClientInclusions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInclusions_Clients_ClientId",
                table: "ClientInclusions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInclusions_Projects_ProjectId",
                table: "ClientInclusions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ClientInclusions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ClientInclusions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("14772961-c4fc-4dcd-9a78-ef20ad55087f"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 57, 20, 50, DateTimeKind.Local).AddTicks(337));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("37426077-ccdf-4043-b7f1-5aef998f1a45"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 57, 20, 50, DateTimeKind.Local).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("6141b753-bfc8-4af2-8912-4b2828459c2b"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 57, 20, 50, DateTimeKind.Local).AddTicks(332));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("e0684bf4-d91b-4099-ac68-0bdbb9bc0a50"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 57, 20, 50, DateTimeKind.Local).AddTicks(308));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("eb6afbf8-63b0-46bd-bb21-ffec9f890fec"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 57, 20, 50, DateTimeKind.Local).AddTicks(334));

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInclusions_Clients_ClientId",
                table: "ClientInclusions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInclusions_Projects_ProjectId",
                table: "ClientInclusions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
