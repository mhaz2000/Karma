using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tablesareseperated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Major_Title",
                table: "EducationalRecords");

            migrationBuilder.DropColumn(
                name: "University_Title",
                table: "EducationalRecords");

            migrationBuilder.DropColumn(
                name: "JobCategory_Title",
                table: "CareerRecords");

            migrationBuilder.RenameColumn(
                name: "University_Id",
                table: "EducationalRecords",
                newName: "UniversityId");

            migrationBuilder.RenameColumn(
                name: "Major_Id",
                table: "EducationalRecords",
                newName: "MajorId");

            migrationBuilder.RenameColumn(
                name: "JobCategory_Id",
                table: "CareerRecords",
                newName: "JobCategoryId");

            migrationBuilder.CreateTable(
                name: "JobCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalRecords_MajorId",
                table: "EducationalRecords",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalRecords_UniversityId",
                table: "EducationalRecords",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerRecords_JobCategoryId",
                table: "CareerRecords",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerRecords_JobCategorys_JobCategoryId",
                table: "CareerRecords",
                column: "JobCategoryId",
                principalTable: "JobCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalRecords_Majors_MajorId",
                table: "EducationalRecords",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalRecords_Universities_UniversityId",
                table: "EducationalRecords",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerRecords_JobCategorys_JobCategoryId",
                table: "CareerRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalRecords_Majors_MajorId",
                table: "EducationalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalRecords_Universities_UniversityId",
                table: "EducationalRecords");

            migrationBuilder.DropTable(
                name: "JobCategorys");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_EducationalRecords_MajorId",
                table: "EducationalRecords");

            migrationBuilder.DropIndex(
                name: "IX_EducationalRecords_UniversityId",
                table: "EducationalRecords");

            migrationBuilder.DropIndex(
                name: "IX_CareerRecords_JobCategoryId",
                table: "CareerRecords");

            migrationBuilder.RenameColumn(
                name: "UniversityId",
                table: "EducationalRecords",
                newName: "University_Id");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "EducationalRecords",
                newName: "Major_Id");

            migrationBuilder.RenameColumn(
                name: "JobCategoryId",
                table: "CareerRecords",
                newName: "JobCategory_Id");

            migrationBuilder.AddColumn<string>(
                name: "Major_Title",
                table: "EducationalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "University_Title",
                table: "EducationalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobCategory_Title",
                table: "CareerRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
