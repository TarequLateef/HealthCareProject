using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class PatientDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContactToBird",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ExSmoker",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lactating",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OccupID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassiveSmoker",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Pregmant",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Smoker",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WorkID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactToBird",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "ExSmoker",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "Lactating",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "OccupID",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "PassiveSmoker",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "Pregmant",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "Smoker",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "WorkID",
                schema: "Patient",
                table: "ParientDataTBL");
        }
    }
}
