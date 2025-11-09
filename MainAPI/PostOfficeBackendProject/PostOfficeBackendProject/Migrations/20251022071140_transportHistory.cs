using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class transportHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliverdTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostOfficeId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    PostmanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transport_PostOffice_PostOfficeId",
                        column: x => x.PostOfficeId,
                        principalTable: "PostOffice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transport_Postman_PostmanId",
                        column: x => x.PostmanId,
                        principalTable: "Postman",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transport_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transport_PostmanId",
                table: "Transport",
                column: "PostmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_PostOfficeId",
                table: "Transport",
                column: "PostOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_ProductId",
                table: "Transport",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transport");
        }
    }
}
