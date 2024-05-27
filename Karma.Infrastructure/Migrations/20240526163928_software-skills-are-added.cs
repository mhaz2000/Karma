using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softwareskillsareadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "SoftwareSkills");

            migrationBuilder.AddColumn<int>(
                name: "SystemSoftwareSkillId",
                table: "SoftwareSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SystemSoftwareSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSoftwareSkills", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSkills_SystemSoftwareSkillId",
                table: "SoftwareSkills",
                column: "SystemSoftwareSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareSkills_SystemSoftwareSkills_SystemSoftwareSkillId",
                table: "SoftwareSkills",
                column: "SystemSoftwareSkillId",
                principalTable: "SystemSoftwareSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareSkills_SystemSoftwareSkills_SystemSoftwareSkillId",
                table: "SoftwareSkills");

            migrationBuilder.DropTable(
                name: "SystemSoftwareSkills");

            migrationBuilder.DropIndex(
                name: "IX_SoftwareSkills_SystemSoftwareSkillId",
                table: "SoftwareSkills");

            migrationBuilder.DropColumn(
                name: "SystemSoftwareSkillId",
                table: "SoftwareSkills");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SoftwareSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
