using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Persistence.Db.Migrations
{
    /// <inheritdoc />
    public partial class SchoolDbContextAddGrade7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "School",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "School",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                schema: "School",
                table: "Grade",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IsActive", "IsDeleted", "NameAr", "NameEn" },
                values: new object[] { 7, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الصف السابع", true, false, "الصف السابع", "Grade 7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "School",
                table: "Grade",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "School",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "School",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
