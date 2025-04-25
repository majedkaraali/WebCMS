using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class UserNagivigationForWorkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LabWorkers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabWorkers_UserId",
                table: "LabWorkers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorkers_AspNetUsers_UserId",
                table: "LabWorkers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorkers_AspNetUsers_UserId",
                table: "LabWorkers");

            migrationBuilder.DropIndex(
                name: "IX_LabWorkers_UserId",
                table: "LabWorkers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LabWorkers");
        }
    }
}
