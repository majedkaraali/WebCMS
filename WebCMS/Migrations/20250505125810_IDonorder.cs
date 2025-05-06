using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class IDonorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabOrders_LabTests_LabTestId",
                table: "LabOrders");

            migrationBuilder.RenameColumn(
                name: "LabTestId",
                table: "LabOrders",
                newName: "LabTestCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_LabOrders_LabTestId",
                table: "LabOrders",
                newName: "IX_LabOrders_LabTestCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabOrders_LabTestCategories_LabTestCategoryId",
                table: "LabOrders",
                column: "LabTestCategoryId",
                principalTable: "LabTestCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabOrders_LabTestCategories_LabTestCategoryId",
                table: "LabOrders");

            migrationBuilder.RenameColumn(
                name: "LabTestCategoryId",
                table: "LabOrders",
                newName: "LabTestId");

            migrationBuilder.RenameIndex(
                name: "IX_LabOrders_LabTestCategoryId",
                table: "LabOrders",
                newName: "IX_LabOrders_LabTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabOrders_LabTests_LabTestId",
                table: "LabOrders",
                column: "LabTestId",
                principalTable: "LabTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
