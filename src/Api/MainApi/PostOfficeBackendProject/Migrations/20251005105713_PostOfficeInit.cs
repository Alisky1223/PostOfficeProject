using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeProject.Core.Migrations
{
    /// <inheritdoc />
    public partial class PostOfficeInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostOffice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeAccessCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOffice", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostOffice");
        }
    }
}
