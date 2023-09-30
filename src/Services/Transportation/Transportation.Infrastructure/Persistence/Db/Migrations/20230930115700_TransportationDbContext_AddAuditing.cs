using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Infrastructure.Persistence.Db.Migrations
{
    /// <inheritdoc />
    public partial class TransportationDbContextAddAuditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReserved",
                schema: "Transportation",
                table: "Bus",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Transportation",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Transportation",
                table: "Driver",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "Transportation",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "Transportation",
                table: "Driver",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Transportation",
                table: "Bus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Transportation",
                table: "Bus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Transportation",
                table: "Bus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReservationStatusId",
                schema: "Transportation",
                table: "Bus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "Transportation",
                table: "Bus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "Transportation",
                table: "Bus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus",
                column: "ReservationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_ReservationStatus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus",
                column: "ReservationStatusId",
                principalSchema: "Transportation",
                principalTable: "ReservationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_ReservationStatus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "Transportation",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "ReservationStatusId",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                schema: "Transportation",
                table: "Bus",
                newName: "IsReserved");
        }
    }
}
