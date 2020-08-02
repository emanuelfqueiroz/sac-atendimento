using Microsoft.EntityFrameworkCore.Migrations;

namespace SAC.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Protocolos_DigitroId",
                table: "Protocolos");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_DigitroId",
                table: "Protocolos",
                column: "DigitroId",
                unique: true,
                filter: "[DigitroId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Protocolos_DigitroId",
                table: "Protocolos");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_DigitroId",
                table: "Protocolos",
                column: "DigitroId");
        }
    }
}
