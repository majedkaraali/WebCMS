using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class @bool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "updated",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated",
                table: "Patients");
        }
    }
}
