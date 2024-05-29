using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class diplomachangesineducationalrecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalRecords_Majors_MajorId",
                table: "EducationalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalRecords_Universities_UniversityId",
                table: "EducationalRecords");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "EducationalRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "EducationalRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FromYear",
                table: "EducationalRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DiplomaMajor",
                table: "EducationalRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalRecords_Majors_MajorId",
                table: "EducationalRecords",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalRecords_Universities_UniversityId",
                table: "EducationalRecords",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalRecords_Majors_MajorId",
                table: "EducationalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalRecords_Universities_UniversityId",
                table: "EducationalRecords");

            migrationBuilder.DropColumn(
                name: "DiplomaMajor",
                table: "EducationalRecords");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "EducationalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "EducationalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromYear",
                table: "EducationalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
