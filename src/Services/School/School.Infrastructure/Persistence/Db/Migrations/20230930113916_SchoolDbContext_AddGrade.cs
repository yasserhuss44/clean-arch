using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School.Infrastructure.Persistence.Db.Migrations
{
    /// <inheritdoc />
    public partial class SchoolDbContextAddGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "School",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "School",
                table: "Student",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                schema: "School",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "School",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "School",
                table: "Student",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "School",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "School",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "School",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "School",
                table: "Course",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grade",
                schema: "School",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "School",
                table: "Grade",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IsActive", "IsDeleted", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف الاول", true, false, "الصف الاول", "Grade 1" },
                    { 2, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف الثاني", true, false, "الصف الثاني", "Grade 2" },
                    { 3, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف الثالث", true, false, "الصف الثالث", "Grade 3" },
                    { 4, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف الرابع", true, false, "الصف الرابع", "Grade 4" },
                    { 5, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف الخامس", true, false, "الصف الخامس", "Grade 5" },
                    { 6, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف السادس", true, false, "الصف السادس", "Grade 6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_GradeId",
                schema: "School",
                table: "Student",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Grade_GradeId",
                schema: "School",
                table: "Student",
                column: "GradeId",
                principalSchema: "School",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Grade_GradeId",
                schema: "School",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Grade",
                schema: "School");

            migrationBuilder.DropIndex(
                name: "IX_Student_GradeId",
                schema: "School",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "School",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "School",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GradeId",
                schema: "School",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "School",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "School",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "School",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "School",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "School",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "School",
                table: "Course");
        }
    }
}
