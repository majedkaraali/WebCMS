using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCMS.Migrations
{
    /// <inheritdoc />
    public partial class NewTestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceRange",
                table: "LabTests");

            migrationBuilder.RenameColumn(
                name: "TestCategory",
                table: "LabTests",
                newName: "NormalValue");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "LabTests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxRange",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinRange",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestCategoryId",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LabTestCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_TestCategoryId",
                table: "LabTests",
                column: "TestCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_LabTestCategory_TestCategoryId",
                table: "LabTests",
                column: "TestCategoryId",
                principalTable: "LabTestCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_LabTestCategory_TestCategoryId",
                table: "LabTests");

            migrationBuilder.DropTable(
                name: "LabTestCategory");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_TestCategoryId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "MaxRange",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "MinRange",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "TestCategoryId",
                table: "LabTests");

            migrationBuilder.RenameColumn(
                name: "NormalValue",
                table: "LabTests",
                newName: "TestCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "LabTests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceRange",
                table: "LabTests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
