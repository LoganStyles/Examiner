using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examiner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CodeVerificationAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "CodeVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "CodeVerifications");
        }
    }
}
