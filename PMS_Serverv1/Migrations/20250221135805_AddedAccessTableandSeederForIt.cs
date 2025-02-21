using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PMS_Serverv1.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccessTableandSeederForIt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    AccessId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.AccessId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAccess",
                columns: table => new
                {
                    UserAccessId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AccessId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccess", x => x.UserAccessId);
                    table.ForeignKey(
                        name: "FK_UserAccess_Access_AccessId",
                        column: x => x.AccessId,
                        principalTable: "Access",
                        principalColumn: "AccessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Access",
                columns: new[] { "AccessId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("14772961-c4fc-4dcd-9a78-ef20ad55087f"), new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9091), "", null, null, "Admin", null, null },
                    { new Guid("37426077-ccdf-4043-b7f1-5aef998f1a45"), new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9089), "", null, null, "Contributor", null, null },
                    { new Guid("6141b753-bfc8-4af2-8912-4b2828459c2b"), new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9086), "", null, null, "User", null, null },
                    { new Guid("e0684bf4-d91b-4099-ac68-0bdbb9bc0a50"), new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9061), "", null, null, "Read", null, null },
                    { new Guid("eb6afbf8-63b0-46bd-bb21-ffec9f890fec"), new DateTime(2025, 2, 21, 21, 58, 5, 173, DateTimeKind.Local).AddTicks(9088), "", null, null, "Encoder", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_AccessId",
                table: "UserAccess",
                column: "AccessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UserId",
                table: "UserAccess",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccess");

            migrationBuilder.DropTable(
                name: "Access");
        }
    }
}
