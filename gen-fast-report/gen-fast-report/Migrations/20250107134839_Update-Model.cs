using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gen_fast_report.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "StandardReports",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 1,
                column: "File",
                value: null);

            migrationBuilder.UpdateData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 2,
                column: "File",
                value: null);

            migrationBuilder.UpdateData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 3,
                column: "File",
                value: null);

            migrationBuilder.UpdateData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 4,
                column: "File",
                value: null);

            migrationBuilder.UpdateData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 5,
                column: "File",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "StandardReports");
        }
    }
}
