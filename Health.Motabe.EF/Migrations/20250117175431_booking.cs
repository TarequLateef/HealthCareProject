using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientID",
                schema: "Patient",
                table: "BookingTBL",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTBL_PatientID",
                schema: "Patient",
                table: "BookingTBL",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTBL_PatientBaseTBL_PatientID",
                schema: "Patient",
                table: "BookingTBL",
                column: "PatientID",
                principalSchema: "Patient",
                principalTable: "PatientBaseTBL",
                principalColumn: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTBL_PatientBaseTBL_PatientID",
                schema: "Patient",
                table: "BookingTBL");

            migrationBuilder.DropIndex(
                name: "IX_BookingTBL_PatientID",
                schema: "Patient",
                table: "BookingTBL");

            migrationBuilder.AlterColumn<string>(
                name: "PatientID",
                schema: "Patient",
                table: "BookingTBL",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
