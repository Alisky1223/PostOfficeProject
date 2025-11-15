using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeProject.Core.Migrations
{
    /// <inheritdoc />
    public partial class trasportStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportStatusId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransportStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_TransportStatusId",
                table: "Product",
                column: "TransportStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_TransportStatus_TransportStatusId",
                table: "Product",
                column: "TransportStatusId",
                principalTable: "TransportStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_TransportStatus_TransportStatusId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "TransportStatus");

            migrationBuilder.DropIndex(
                name: "IX_Product_TransportStatusId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TransportStatusId",
                table: "Product");
        }
    }
}
