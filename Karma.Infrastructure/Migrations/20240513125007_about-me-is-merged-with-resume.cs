using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aboutmeismergedwithresume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerRecords_JobCategorys_JobCategoryId",
                table: "CareerRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AboutMes_AboutMeId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_AboutMes_AboutMeId",
                table: "SocialMedias");

            migrationBuilder.DropTable(
                name: "AboutMes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_AboutMeId",
                table: "Resumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategorys",
                table: "JobCategorys");

            migrationBuilder.DropColumn(
                name: "AboutMeId",
                table: "Resumes");

            migrationBuilder.RenameTable(
                name: "JobCategorys",
                newName: "JobCategories");

            migrationBuilder.RenameColumn(
                name: "AboutMeId",
                table: "SocialMedias",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMedias_AboutMeId",
                table: "SocialMedias",
                newName: "IX_SocialMedias_ResumeId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainJobTitle",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerRecords_JobCategories_JobCategoryId",
                table: "CareerRecords",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_Resumes_ResumeId",
                table: "SocialMedias",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerRecords_JobCategories_JobCategoryId",
                table: "CareerRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_Resumes_ResumeId",
                table: "SocialMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "MainJobTitle",
                table: "Resumes");

            migrationBuilder.RenameTable(
                name: "JobCategories",
                newName: "JobCategorys");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "SocialMedias",
                newName: "AboutMeId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMedias_ResumeId",
                table: "SocialMedias",
                newName: "IX_SocialMedias_AboutMeId");

            migrationBuilder.AddColumn<Guid>(
                name: "AboutMeId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategorys",
                table: "JobCategorys",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AboutMes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainJobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutMes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AboutMeId",
                table: "Resumes",
                column: "AboutMeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerRecords_JobCategorys_JobCategoryId",
                table: "CareerRecords",
                column: "JobCategoryId",
                principalTable: "JobCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AboutMes_AboutMeId",
                table: "Resumes",
                column: "AboutMeId",
                principalTable: "AboutMes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_AboutMes_AboutMeId",
                table: "SocialMedias",
                column: "AboutMeId",
                principalTable: "AboutMes",
                principalColumn: "Id");
        }
    }
}
