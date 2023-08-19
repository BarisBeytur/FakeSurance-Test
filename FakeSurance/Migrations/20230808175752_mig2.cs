using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeSurance.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCoverages_Coverages_CoverageId",
                table: "ProductCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Products_ProductId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_CustomerId",
                table: "Proposals");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Proposals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CoverageId",
                table: "ProductCoverages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_CustomerId",
                table: "Proposals",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCoverages_Coverages_CoverageId",
                table: "ProductCoverages",
                column: "CoverageId",
                principalTable: "Coverages",
                principalColumn: "CoverageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Products_ProductId",
                table: "Proposals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCoverages_Coverages_CoverageId",
                table: "ProductCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Products_ProductId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_CustomerId",
                table: "Proposals");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CoverageId",
                table: "ProductCoverages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_CustomerId",
                table: "Proposals",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCoverages_Coverages_CoverageId",
                table: "ProductCoverages",
                column: "CoverageId",
                principalTable: "Coverages",
                principalColumn: "CoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Products_ProductId",
                table: "Proposals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
