using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examiner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CodeVerificationExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanResend",
                table: "CodeVerifications");

            migrationBuilder.AddColumn<int>(
                name: "ExpiresIn",
                table: "CodeVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresIn",
                table: "CodeVerifications");

            migrationBuilder.AddColumn<bool>(
                name: "CanResend",
                table: "CodeVerifications",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
