using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeDate",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "CityID",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "CountryID",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "GovernID",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "Occup",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "OtherPhone",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "PatientCode",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.DropColumn(
                name: "WorkID",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.AddColumn<DateTime>(
                name: "AgeDate",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CityID",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryID",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GovernID",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Occup",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherPhone",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientCode",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkID",
                schema: "Patient",
                table: "PatientBaseTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeDate",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "CityID",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "CountryID",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "GovernID",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "Occup",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "OtherPhone",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "PatientCode",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.DropColumn(
                name: "WorkID",
                schema: "Patient",
                table: "PatientBaseTBL");

            migrationBuilder.AddColumn<DateTime>(
                name: "AgeDate",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CityID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GovernID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Occup",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherPhone",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientCode",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
