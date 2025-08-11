using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class initClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Patient");

            migrationBuilder.EnsureSchema(
                name: "Service");

            migrationBuilder.CreateTable(
                name: "BookingTBL",
                schema: "Patient",
                columns: table => new
                {
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    Repeated = table.Column<bool>(type: "bit", nullable: false),
                    CompID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTBL", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticTBL",
                schema: "Service",
                columns: table => new
                {
                    DiagID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiagnosticName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CompTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticTBL", x => x.DiagID);
                });

            migrationBuilder.CreateTable(
                name: "PatientBaseTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CompID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientBaseTBL", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "SymptomsTBL",
                schema: "Service",
                columns: table => new
                {
                    SymptomID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SymptonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CompTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomsTBL", x => x.SymptomID);
                });

            migrationBuilder.CreateTable(
                name: "ParientDataTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryID = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    GovernID = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CityID = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtherPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PatientCode = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParientDataTBL", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_ParientDataTBL_PatientBaseTBL_PatientID",
                        column: x => x.PatientID,
                        principalSchema: "Patient",
                        principalTable: "PatientBaseTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatDiagTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatDiagID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiagID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrimaryDiag = table.Column<bool>(type: "bit", nullable: false),
                    BookingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatDiagTBL", x => x.PatDiagID);
                    table.ForeignKey(
                        name: "FK_PatDiagTBL_BookingTBL_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatDiagTBL_DiagnosticTBL_DiagID",
                        column: x => x.DiagID,
                        principalSchema: "Service",
                        principalTable: "DiagnosticTBL",
                        principalColumn: "DiagID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatDiagTBL_ParientDataTBL_PatID",
                        column: x => x.PatID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatSympTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatSympID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SympID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SympStatus = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CompID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatSympTBL", x => x.PatSympID);
                    table.ForeignKey(
                        name: "FK_PatSympTBL_BookingTBL_BookID",
                        column: x => x.BookID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatSympTBL_ParientDataTBL_PatID",
                        column: x => x.PatID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatSympTBL_SymptomsTBL_SympID",
                        column: x => x.SympID,
                        principalSchema: "Service",
                        principalTable: "SymptomsTBL",
                        principalColumn: "SymptomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatDiagTBL_BookingID",
                schema: "Patient",
                table: "PatDiagTBL",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_PatDiagTBL_DiagID",
                schema: "Patient",
                table: "PatDiagTBL",
                column: "DiagID");

            migrationBuilder.CreateIndex(
                name: "IX_PatDiagTBL_PatID",
                schema: "Patient",
                table: "PatDiagTBL",
                column: "PatID");

            migrationBuilder.CreateIndex(
                name: "IX_PatSympTBL_BookID",
                schema: "Patient",
                table: "PatSympTBL",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_PatSympTBL_PatID",
                schema: "Patient",
                table: "PatSympTBL",
                column: "PatID");

            migrationBuilder.CreateIndex(
                name: "IX_PatSympTBL_SympID",
                schema: "Patient",
                table: "PatSympTBL",
                column: "SympID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatDiagTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "PatSympTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "DiagnosticTBL",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "BookingTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "ParientDataTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "SymptomsTBL",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "PatientBaseTBL",
                schema: "Patient");
        }
    }
}
