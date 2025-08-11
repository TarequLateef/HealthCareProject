using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class editOccup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccupID",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.AddColumn<bool>(
                name: "Occup",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occup",
                schema: "Patient",
                table: "ParientDataTBL");

            migrationBuilder.AddColumn<string>(
                name: "OccupID",
                schema: "Patient",
                table: "ParientDataTBL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
