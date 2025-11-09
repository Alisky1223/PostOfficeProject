using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class userTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customer");
        }
    }
}
