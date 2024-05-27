using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softwareskilllevelismodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SofwareSkillLevel",
                table: "SoftwareSkills",
                newName: "SoftwareSkillLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoftwareSkillLevel",
                table: "SoftwareSkills",
                newName: "SofwareSkillLevel");
        }
    }
}
