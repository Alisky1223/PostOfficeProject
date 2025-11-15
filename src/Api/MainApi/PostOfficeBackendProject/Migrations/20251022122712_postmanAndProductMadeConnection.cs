using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeProject.Core.Migrations
{
    /// <inheritdoc />
    public partial class postmanAndProductMadeConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostmanId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_PostmanId",
                table: "Product",
                column: "PostmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Postman_PostmanId",
                table: "Product",
                column: "PostmanId",
                principalTable: "Postman",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Postman_PostmanId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_PostmanId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PostmanId",
                table: "Product");
        }
    }
}
