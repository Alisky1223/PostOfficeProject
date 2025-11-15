using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeProject.Core.Migrations
{
    /// <inheritdoc />
    public partial class addPostman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Postman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOfficeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postman", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postman_PostOffice_PostOfficeId",
                        column: x => x.PostOfficeId,
                        principalTable: "PostOffice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postman_PostOfficeId",
                table: "Postman",
                column: "PostOfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postman");
        }
    }
}
