using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class agetoDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.AlterColumn<bool>(
                name: "PassiveSmoker",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "AgeDate",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeDate",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.AlterColumn<string>(
                name: "PassiveSmoker",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
