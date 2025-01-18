using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gen_fast_report.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StandardReports",
                columns: new[] { "Id", "Area", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Padrao Balistica" },
                    { 2, 6, "Padrao Trânsito" },
                    { 3, 5, "Padrao Vida" },
                    { 4, 3, "Padrao Documentoscopia" },
                    { 5, 4, "Padrao Meio Ambiente" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StandardReports",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
