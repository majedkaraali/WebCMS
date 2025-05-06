using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class NewTestModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_LabTestCategory_TestCategoryId",
                table: "LabTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabTestCategory",
                table: "LabTestCategory");

            migrationBuilder.RenameTable(
                name: "LabTestCategory",
                newName: "LabTestCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabTestCategories",
                table: "LabTestCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_LabTestCategories_TestCategoryId",
                table: "LabTests",
                column: "TestCategoryId",
                principalTable: "LabTestCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_LabTestCategories_TestCategoryId",
                table: "LabTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabTestCategories",
                table: "LabTestCategories");

            migrationBuilder.RenameTable(
                name: "LabTestCategories",
                newName: "LabTestCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabTestCategory",
                table: "LabTestCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_LabTestCategory_TestCategoryId",
                table: "LabTests",
                column: "TestCategoryId",
                principalTable: "LabTestCategory",
                principalColumn: "Id");
        }
    }
}
