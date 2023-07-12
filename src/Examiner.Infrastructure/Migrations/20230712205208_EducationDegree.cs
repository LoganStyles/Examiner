using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Examiner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EducationDegree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "CountryCode",
            //     table: "UserProfiles");

            // migrationBuilder.DropColumn(
            //     name: "Location",
            //     table: "UserProfiles");

            // migrationBuilder.AddColumn<string>(
            //     name: "Address",
            //     table: "UserProfiles",
            //     type: "longtext",
            //     nullable: true)
            //     .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.AddColumn<int>(
            //     name: "CountryId",
            //     table: "UserProfiles",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);

            // migrationBuilder.AddColumn<int>(
            //     name: "ExperienceLevelId",
            //     table: "UserProfiles",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);

            // migrationBuilder.AddColumn<bool>(
            //     name: "IsAvailable",
            //     table: "UserProfiles",
            //     type: "tinyint(1)",
            //     nullable: false,
            //     defaultValue: false);

            // migrationBuilder.AddColumn<int>(
            //     name: "StateId",
            //     table: "UserProfiles",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EducationDegrees",
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
                    table.PrimaryKey("PK_EducationDegrees", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.UpdateData(
            //     table: "Countries",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4062));

            migrationBuilder.InsertData(
                table: "EducationDegrees",
                columns: new[] { "Id", "CreatedDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4273), "Ordinary National Diploma" },
                    { 2, new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4300), "Higher National Diploma" },
                    { 3, new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4322), "Bachelor's Degree" },
                    { 4, new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4345), "Postgraduate Diploma" },
                    { 5, new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4372), "Masters" },
                    { 6, new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4399), "PHD" }
                });

            // migrationBuilder.UpdateData(
            //     table: "ExperienceLevels",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4192));

            // migrationBuilder.UpdateData(
            //     table: "ExperienceLevels",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4218));

            // migrationBuilder.UpdateData(
            //     table: "ExperienceLevels",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4241));

            // migrationBuilder.UpdateData(
            //     table: "States",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4099));

            // migrationBuilder.UpdateData(
            //     table: "States",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4127));

            // migrationBuilder.UpdateData(
            //     table: "States",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4159));

            // migrationBuilder.UpdateData(
            //     table: "SubjectCategories",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3478));

            // migrationBuilder.UpdateData(
            //     table: "SubjectCategories",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3578));

            // migrationBuilder.UpdateData(
            //     table: "SubjectCategories",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3705));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3759));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3803));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3852));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 4,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3877));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 5,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3918));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 6,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3952));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 7,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(3977));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 8,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4002));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 9,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 52, 7, 338, DateTimeKind.Local).AddTicks(4024));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationDegrees");

            // migrationBuilder.DropColumn(
            //     name: "Address",
            //     table: "UserProfiles");

            // migrationBuilder.DropColumn(
            //     name: "CountryId",
            //     table: "UserProfiles");

            // migrationBuilder.DropColumn(
            //     name: "ExperienceLevelId",
            //     table: "UserProfiles");

            // migrationBuilder.DropColumn(
            //     name: "IsAvailable",
            //     table: "UserProfiles");

            // migrationBuilder.DropColumn(
            //     name: "StateId",
            //     table: "UserProfiles");

            // migrationBuilder.AddColumn<string>(
            //     name: "CountryCode",
            //     table: "UserProfiles",
            //     type: "varchar(3)",
            //     maxLength: 3,
            //     nullable: true)
            //     .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.AddColumn<string>(
            //     name: "Location",
            //     table: "UserProfiles",
            //     type: "varchar(50)",
            //     maxLength: 50,
            //     nullable: true)
            //     .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.UpdateData(
            //     table: "Countries",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6965));

            // migrationBuilder.UpdateData(
            //     table: "ExperienceLevels",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7046));

            // migrationBuilder.UpdateData(
            //     table: "ExperienceLevels",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7063));

            // migrationBuilder.UpdateData(
            //     table: "ExperienceLevels",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7081));

            // migrationBuilder.UpdateData(
            //     table: "States",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6994));

            // migrationBuilder.UpdateData(
            //     table: "States",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7011));

            // migrationBuilder.UpdateData(
            //     table: "States",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(7025));

            // migrationBuilder.UpdateData(
            //     table: "SubjectCategories",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6645));

            // migrationBuilder.UpdateData(
            //     table: "SubjectCategories",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6722));

            // migrationBuilder.UpdateData(
            //     table: "SubjectCategories",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6755));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 1,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6783));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 2,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6807));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 3,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6820));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 4,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6834));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 5,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6848));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 6,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6890));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 7,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6905));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 8,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6922));

            // migrationBuilder.UpdateData(
            //     table: "Subjects",
            //     keyColumn: "Id",
            //     keyValue: 9,
            //     column: "CreatedDate",
            //     value: new DateTime(2023, 7, 12, 21, 14, 56, 819, DateTimeKind.Local).AddTicks(6936));
        }
    }
}
