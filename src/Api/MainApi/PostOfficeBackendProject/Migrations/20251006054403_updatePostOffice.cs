using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeProject.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatePostOffice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PostOffice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StorageCapacity",
                table: "PostOffice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PostOffice");

            migrationBuilder.DropColumn(
                name: "StorageCapacity",
                table: "PostOffice");
        }
    }
}
