using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class updatedAllergyFeildPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Patients_PatientId",
                table: "Allergies");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_PatientId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Allergies");

            migrationBuilder.AddColumn<string>(
                name: "SelectedAllergyIds",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAllergyIds",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Allergies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_PatientId",
                table: "Allergies",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Patients_PatientId",
                table: "Allergies",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
