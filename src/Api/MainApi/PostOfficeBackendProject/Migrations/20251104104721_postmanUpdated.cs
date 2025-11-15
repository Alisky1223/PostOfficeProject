using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeProject.Core.Migrations
{
    /// <inheritdoc />
    public partial class postmanUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Postman",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Postman");
        }
    }
}
