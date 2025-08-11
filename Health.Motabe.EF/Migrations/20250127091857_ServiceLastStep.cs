using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Motabe.EF.Migrations
{
    /// <inheritdoc />
    public partial class ServiceLastStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisTBL",
                schema: "Service",
                columns: table => new
                {
                    AnalysisID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnalysisName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisTBL", x => x.AnalysisID);
                });

            migrationBuilder.CreateTable(
                name: "BiometricTBL",
                schema: "Service",
                columns: table => new
                {
                    BioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BioName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalMeasure = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiometricTBL", x => x.BioID);
                });

            migrationBuilder.CreateTable(
                name: "CT_TBL",
                schema: "Service",
                columns: table => new
                {
                    CT_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CT_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_TBL", x => x.CT_ID);
                });

            migrationBuilder.CreateTable(
                name: "MedicineTBL",
                schema: "Service",
                columns: table => new
                {
                    MedID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MedType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineTBL", x => x.MedID);
                });

            migrationBuilder.CreateTable(
                name: "RaysTBL",
                schema: "Service",
                columns: table => new
                {
                    RayID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RayName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descirption = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaysTBL", x => x.RayID);
                });

            migrationBuilder.CreateTable(
                name: "PatAnalysisTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatAnalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnalysisID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnalResult = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LabName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatAnalysisTBL", x => x.PatAnalID);
                    table.ForeignKey(
                        name: "FK_PatAnalysisTBL_AnalysisTBL_AnalysisID",
                        column: x => x.AnalysisID,
                        principalSchema: "Service",
                        principalTable: "AnalysisTBL",
                        principalColumn: "AnalysisID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatAnalysisTBL_BookingTBL_BookID",
                        column: x => x.BookID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatAnalysisTBL_ParientDataTBL_PatID",
                        column: x => x.PatID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatBioTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatBioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BioMeasure = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatBioTBL", x => x.PatBioID);
                    table.ForeignKey(
                        name: "FK_PatBioTBL_BiometricTBL_BioID",
                        column: x => x.BioID,
                        principalSchema: "Service",
                        principalTable: "BiometricTBL",
                        principalColumn: "BioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatBioTBL_BookingTBL_BookID",
                        column: x => x.BookID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatBioTBL_ParientDataTBL_PatID",
                        column: x => x.PatID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatCt_TBL",
                schema: "Patient",
                columns: table => new
                {
                    PatCtID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CTID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CtResult = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatCt_TBL", x => x.PatCtID);
                    table.ForeignKey(
                        name: "FK_PatCt_TBL_BookingTBL_BookID",
                        column: x => x.BookID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatCt_TBL_CT_TBL_CTID",
                        column: x => x.CTID,
                        principalSchema: "Service",
                        principalTable: "CT_TBL",
                        principalColumn: "CT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatCt_TBL_ParientDataTBL_PatID",
                        column: x => x.PatID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatMedicineTBL",
                schema: "Patient",
                columns: table => new
                {
                    PatMedID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Period = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatMedicineTBL", x => x.PatMedID);
                    table.ForeignKey(
                        name: "FK_PatMedicineTBL_BookingTBL_BookID",
                        column: x => x.BookID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatMedicineTBL_MedicineTBL_MedID",
                        column: x => x.MedID,
                        principalSchema: "Service",
                        principalTable: "MedicineTBL",
                        principalColumn: "MedID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatMedicineTBL_ParientDataTBL_PatID",
                        column: x => x.PatID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatRaysTBL",
                schema: "Patient",
                columns: table => new
                {
                    PRID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RayID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RayNote = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Descirption = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    X_Lab = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserLogID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatRaysTBL", x => x.PRID);
                    table.ForeignKey(
                        name: "FK_PatRaysTBL_BookingTBL_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Patient",
                        principalTable: "BookingTBL",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatRaysTBL_ParientDataTBL_PatientID",
                        column: x => x.PatientID,
                        principalSchema: "Patient",
                        principalTable: "ParientDataTBL",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatRaysTBL_RaysTBL_RayID",
                        column: x => x.RayID,
                        principalSchema: "Service",
                        principalTable: "RaysTBL",
                        principalColumn: "RayID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatAnalysisTBL_AnalysisID",
                schema: "Patient",
                table: "PatAnalysisTBL",
                column: "AnalysisID");

            migrationBuilder.CreateIndex(
                name: "IX_PatAnalysisTBL_BookID",
                schema: "Patient",
                table: "PatAnalysisTBL",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_PatAnalysisTBL_PatID",
                schema: "Patient",
                table: "PatAnalysisTBL",
                column: "PatID");

            migrationBuilder.CreateIndex(
                name: "IX_PatBioTBL_BioID",
                schema: "Patient",
                table: "PatBioTBL",
                column: "BioID");

            migrationBuilder.CreateIndex(
                name: "IX_PatBioTBL_BookID",
                schema: "Patient",
                table: "PatBioTBL",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_PatBioTBL_PatID",
                schema: "Patient",
                table: "PatBioTBL",
                column: "PatID");

            migrationBuilder.CreateIndex(
                name: "IX_PatCt_TBL_BookID",
                schema: "Patient",
                table: "PatCt_TBL",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_PatCt_TBL_CTID",
                schema: "Patient",
                table: "PatCt_TBL",
                column: "CTID");

            migrationBuilder.CreateIndex(
                name: "IX_PatCt_TBL_PatID",
                schema: "Patient",
                table: "PatCt_TBL",
                column: "PatID");

            migrationBuilder.CreateIndex(
                name: "IX_PatMedicineTBL_BookID",
                schema: "Patient",
                table: "PatMedicineTBL",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_PatMedicineTBL_MedID",
                schema: "Patient",
                table: "PatMedicineTBL",
                column: "MedID");

            migrationBuilder.CreateIndex(
                name: "IX_PatMedicineTBL_PatID",
                schema: "Patient",
                table: "PatMedicineTBL",
                column: "PatID");

            migrationBuilder.CreateIndex(
                name: "IX_PatRaysTBL_BookingID",
                schema: "Patient",
                table: "PatRaysTBL",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_PatRaysTBL_PatientID",
                schema: "Patient",
                table: "PatRaysTBL",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PatRaysTBL_RayID",
                schema: "Patient",
                table: "PatRaysTBL",
                column: "RayID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatAnalysisTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "PatBioTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "PatCt_TBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "PatMedicineTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "PatRaysTBL",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "AnalysisTBL",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "BiometricTBL",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "CT_TBL",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "MedicineTBL",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "RaysTBL",
                schema: "Service");
        }
    }
}
