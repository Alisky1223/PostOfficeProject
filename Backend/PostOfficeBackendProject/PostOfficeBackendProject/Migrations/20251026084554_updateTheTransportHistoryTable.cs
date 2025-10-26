using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class updateTheTransportHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliverdTo",
                table: "Transport",
                newName: "DeliverCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliverCode",
                table: "Transport",
                newName: "DeliverdTo");
        }
    }
}
