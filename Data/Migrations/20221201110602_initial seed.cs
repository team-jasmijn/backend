using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class initialseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProfilesettingsOptions",
                columns: new[] { "Id", "Key", "ModifyDate", "Value" },
                values: new object[,]
                {
                    { -12, "EducationLevel", null, null },
                    { -11, "School", null, null },
                    { -10, "Experience", null, null },
                    { -9, "Goals", null, null },
                    { -8, "Education", null, null },
                    { -7, "Description", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Hash", "ModifyDate", "Name", "Salt", "TimeZoneId", "UserType" },
                values: new object[] { 1, "artimmerman@landstede.nl", "$2a$11$A1PAL2tcek6yMqg8VVVzauteuOFGnD1S4DoqlPP/Hf9ulBwfJTS8y", null, "Arjan Timmerman", "$2a$11$FIhli04K3CDnTp4ObcYIb.", "Africa/Abidjan", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "ProfilesettingsOptions",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
