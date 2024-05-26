using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class systemlanguagesisadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Languages");

            migrationBuilder.AddColumn<int>(
                name: "SystemLanguageId",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SystemLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLanguages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_SystemLanguageId",
                table: "Languages",
                column: "SystemLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_SystemLanguages_SystemLanguageId",
                table: "Languages",
                column: "SystemLanguageId",
                principalTable: "SystemLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_SystemLanguages_SystemLanguageId",
                table: "Languages");

            migrationBuilder.DropTable(
                name: "SystemLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_SystemLanguageId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "SystemLanguageId",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
