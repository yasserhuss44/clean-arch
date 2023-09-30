using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Transportation.Infrastructure.Persistence.Db.Migrations
{
    /// <inheritdoc />
    public partial class TransportationDbContextAddReservationStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.CreateTable(
                name: "ReservationStatus",
                schema: "Transportation",
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
                    table.PrimaryKey("PK_ReservationStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Transportation",
                table: "ReservationStatus",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IsActive", "IsDeleted", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 100, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "محجوز", true, false, "محجوز", "Reserved" },
                    { 200, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "غير محجوز", true, false, "غير محجوز", "Not Reserved" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationStatus",
                schema: "Transportation");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Transportation",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Transportation",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Transportation",
                table: "Bus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Transportation",
                table: "Bus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
