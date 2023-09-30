using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Infrastructure.Persistence.Db.Migrations
{
    /// <inheritdoc />
    public partial class TransportationDbContextAddAuditing2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_ReservationStatus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatusId",
                schema: "Transportation",
                table: "Bus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_ReservationStatus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus",
                column: "ReservationStatusId",
                principalSchema: "Transportation",
                principalTable: "ReservationStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_ReservationStatus_ReservationStatusId",
                schema: "Transportation",
                table: "Bus");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatusId",
                schema: "Transportation",
                table: "Bus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
