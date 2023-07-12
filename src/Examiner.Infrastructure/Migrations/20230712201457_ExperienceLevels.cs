using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Examiner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExperienceLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceLevels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6965));

            migrationBuilder.InsertData(
                table: "ExperienceLevels",
                columns: new[] { "Id", "CreatedDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7046), "Low" },
                    { 2, new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7063), "Moderate" },
                    { 3, new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7081), "High" }
                });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7011));

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7025));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6645));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6722));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6755));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6783));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6807));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6820));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6848));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6890));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6905));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6922));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6936));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceLevels");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(592));

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(649));

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(750));

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(765));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(117));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(187));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(241));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(432));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(451));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(465));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(485));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(508));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(536));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(550));
        }
    }
}
