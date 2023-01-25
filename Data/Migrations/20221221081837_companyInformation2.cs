using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class companyInformation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -14);

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -26,
                column: "Key",
                value: "City");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -25,
                column: "Key",
                value: "CompanyName");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -24,
                column: "Key",
                value: "WorkCulture");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -23,
                column: "Key",
                value: "Hobbies");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -22,
                column: "Key",
                value: "Qualities");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -21,
                column: "Key",
                value: "EducationLevel");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -20,
                column: "Key",
                value: "School");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -19,
                column: "Key",
                value: "Experience");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -18,
                column: "Key",
                value: "Goals");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -17,
                column: "Key",
                value: "Education");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -16,
                column: "Key",
                value: "Description");

            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate" },
                values: new object[,]
                {
                    { -30, "Recap", null },
                    { -29, "ActiveIn", null },
                    { -28, "WorkWise", null },
                    { -27, "LookingFor", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -30);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -29);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -28);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -27);

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -26,
                column: "Key",
                value: "WorkWise");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -25,
                column: "Key",
                value: "LookingFor");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -24,
                column: "Key",
                value: "City");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -23,
                column: "Key",
                value: "CompanyName");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -22,
                column: "Key",
                value: "WorkCulture");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -21,
                column: "Key",
                value: "Hobbies");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -20,
                column: "Key",
                value: "Qualities");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -19,
                column: "Key",
                value: "EducationLevel");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -18,
                column: "Key",
                value: "School");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -17,
                column: "Key",
                value: "Experience");

            migrationBuilder.UpdateData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -16,
                column: "Key",
                value: "Goals");

            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate" },
                values: new object[,]
                {
                    { -15, "Education", null },
                    { -14, "Description", null }
                });
        }
    }
}
