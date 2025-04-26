using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class newFieldsForPatient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialSecurityNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNumber",
                table: "Patients");
        }
    }
}
