using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aboutmeisadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "AboutMeId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AboutMes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MainJobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutMes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SocialMediaType = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutMeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedias_AboutMes_AboutMeId",
                        column: x => x.AboutMeId,
                        principalTable: "AboutMes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AboutMeId",
                table: "Resumes",
                column: "AboutMeId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_AboutMeId",
                table: "SocialMedias",
                column: "AboutMeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AboutMes_AboutMeId",
                table: "Resumes",
                column: "AboutMeId",
                principalTable: "AboutMes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AboutMes_AboutMeId",
                table: "Resumes");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "AboutMes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_AboutMeId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "AboutMeId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Major_Title",
                table: "EducationalRecords");

            migrationBuilder.DropColumn(
                name: "University_Title",
                table: "EducationalRecords");

            migrationBuilder.DropColumn(
                name: "JobCategory_Title",
                table: "CareerRecords");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AspNetUsers");

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
    }
}
