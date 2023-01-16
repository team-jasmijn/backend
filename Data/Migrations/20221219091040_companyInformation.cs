using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class companyInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -13);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -12);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -11);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -16,
                column: "Key",
                value: "Goals");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -15,
                column: "Key",
                value: "Education");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -14,
                column: "Key",
                value: "Description");

            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate" },
                values: new object[,]
                {
                    { -26, "WorkWise", null },
                    { -25, "LookingFor", null },
                    { -24, "City", null },
                    { -23, "CompanyName", null },
                    { -22, "WorkCulture", null },
                    { -21, "Hobbies", null },
                    { -20, "Qualities", null },
                    { -19, "EducationLevel", null },
                    { -18, "School", null },
                    { -17, "Experience", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -26);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -25);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -24);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -23);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -22);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -21);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -20);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -19);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -18);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -17);

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -16,
                column: "Key",
                value: "Hobbies");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -15,
                column: "Key",
                value: "Qualities");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -14,
                column: "Key",
                value: "EducationLevel");

            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate" },
                values: new object[,]
                {
                    { -13, "School", null },
                    { -12, "Experience", null },
                    { -11, "Goals", null },
                    { -10, "Education", null },
                    { -9, "Description", null }
                });
        }
    }
}
