using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCompToServ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompID",
                schema: "Patient",
                table: "PatRaysTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompID",
                schema: "Patient",
                table: "PatMedicineTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompID",
                schema: "Patient",
                table: "PatCt_TBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompID",
                schema: "Patient",
                table: "PatBioTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompID",
                schema: "Patient",
                table: "PatAnalysisTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompID",
                schema: "Patient",
                table: "PatRaysTBL");

            migrationBuilder.DropColumn(
                name: "CompID",
                schema: "Patient",
                table: "PatMedicineTBL");

            migrationBuilder.DropColumn(
                name: "CompID",
                schema: "Patient",
                table: "PatCt_TBL");

            migrationBuilder.DropColumn(
                name: "CompID",
                schema: "Patient",
                table: "PatBioTBL");

            migrationBuilder.DropColumn(
                name: "CompID",
                schema: "Patient",
                table: "PatAnalysisTBL");
        }
    }
}
