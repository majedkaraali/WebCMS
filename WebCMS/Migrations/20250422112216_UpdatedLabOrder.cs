using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLabOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabTestName",
                table: "LabOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Interpretation",
                table: "LabTestResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LabTestId",
                table: "LabOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabOrders_LabTestId",
                table: "LabOrders",
                column: "LabTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabOrders_LabTests_LabTestId",
                table: "LabOrders",
                column: "LabTestId",
                principalTable: "LabTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabOrders_LabTests_LabTestId",
                table: "LabOrders");

            migrationBuilder.DropIndex(
                name: "IX_LabOrders_LabTestId",
                table: "LabOrders");

            migrationBuilder.DropColumn(
                name: "LabTestId",
                table: "LabOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Interpretation",
                table: "LabTestResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabTestName",
                table: "LabOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
