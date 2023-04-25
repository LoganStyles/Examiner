using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examiner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCodeVerificationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeVerificationHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeVerificationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CodeVerificationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeVerificationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeVerificationHistories_CodeVerifications_CodeVerification~",
                        column: x => x.CodeVerificationId,
                        principalTable: "CodeVerifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CodeVerificationHistories_CodeVerificationId",
                table: "CodeVerificationHistories",
                column: "CodeVerificationId");
        }
    }
}
