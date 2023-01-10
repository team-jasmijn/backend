using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedmoreprofilesettingoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -12,
                column: "Key",
                value: "Experience");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -11,
                column: "Key",
                value: "Goals");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -10,
                column: "Key",
                value: "Education");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -9,
                column: "Key",
                value: "Description");

            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate" },
                values: new object[,]
                {
                    { -16, "Hobbies", null },
                    { -15, "Qualities", null },
                    { -14, "EducationLevel", null },
                    { -13, "School", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -16);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -14);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -13);

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -12,
                column: "Key",
                value: "EducationLevel");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -11,
                column: "Key",
                value: "School");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -10,
                column: "Key",
                value: "Experience");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -9,
                column: "Key",
                value: "Goals");

            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate" },
                values: new object[,]
                {
                    { -8, "Education", null },
                    { -7, "Description", null }
                });
        }
    }
}
