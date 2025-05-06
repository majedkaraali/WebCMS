using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class Solving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientsWithDiseases");

            migrationBuilder.CreateTable(
                name: "PatiensDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICD10Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bydoctor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosedbyDr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatiensDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatiensDiseases_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PatiensDiseases_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatiensDiseases_DoctorId",
                table: "PatiensDiseases",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatiensDiseases_PatientId",
                table: "PatiensDiseases",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatiensDiseases");

            migrationBuilder.CreateTable(
                name: "PatientsWithDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Bydoctor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiagnosedbyDr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICD10Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsWithDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientsWithDiseases_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientsWithDiseases_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientsWithDiseases_DoctorId",
                table: "PatientsWithDiseases",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsWithDiseases_PatientId",
                table: "PatientsWithDiseases",
                column: "PatientId");
        }
    }
}
