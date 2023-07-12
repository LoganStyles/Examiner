using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Examiner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CountryAndState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedDate", "Title" },
                values: new object[] { 1, "NG", new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(592), "Nigeria" });

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

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "CountryId", "CreatedDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(649), "Abia" },
                    { 2, 1, new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(750), "Adamawa" },
                    { 3, 1, new DateTime(2023, 7, 12, 19, 29, 12, 739, DateTimeKind.Local).AddTicks(765), "AkwaIbom" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1314));

            migrationBuilder.UpdateData(
                table: "SubjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1342));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1380));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1416));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1461));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1488));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1515));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1564));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1593));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1654));
        }
    }
}
