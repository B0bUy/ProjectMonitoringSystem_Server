using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS_Serverv1.Migrations
{
    /// <inheritdoc />
    public partial class OverrideSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TempProperty",
                table: "Tasks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("14772961-c4fc-4dcd-9a78-ef20ad55087f"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 55, 43, 565, DateTimeKind.Local).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("37426077-ccdf-4043-b7f1-5aef998f1a45"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 55, 43, 565, DateTimeKind.Local).AddTicks(2385));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("6141b753-bfc8-4af2-8912-4b2828459c2b"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 55, 43, 565, DateTimeKind.Local).AddTicks(2381));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("e0684bf4-d91b-4099-ac68-0bdbb9bc0a50"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 55, 43, 565, DateTimeKind.Local).AddTicks(2355));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("eb6afbf8-63b0-46bd-bb21-ffec9f890fec"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 10, 55, 43, 565, DateTimeKind.Local).AddTicks(2383));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempProperty",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("14772961-c4fc-4dcd-9a78-ef20ad55087f"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9091));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("37426077-ccdf-4043-b7f1-5aef998f1a45"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9089));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("6141b753-bfc8-4af2-8912-4b2828459c2b"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9086));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("e0684bf4-d91b-4099-ac68-0bdbb9bc0a50"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "Access",
                keyColumn: "AccessId",
                keyValue: new Guid("eb6afbf8-63b0-46bd-bb21-ffec9f890fec"),
                column: "CreatedAt",
                value: new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9088));
        }
    }
}
