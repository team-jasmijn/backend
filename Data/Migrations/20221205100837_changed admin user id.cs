using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class changedadminuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Hash", "ModifyDate", "Name", "Salt", "TimeZoneId", "UserType" },
                values: new object[] { -1, "artimmerman@landstede.nl", "$2a$11$A1PAL2tcek6yMqg8VVVzauteuOFGnD1S4DoqlPP/Hf9ulBwfJTS8y", null, "Arjan Timmerman", "$2a$11$FIhli04K3CDnTp4ObcYIb.", "Africa/Abidjan", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Hash", "ModifyDate", "Name", "Salt", "TimeZoneId", "UserType" },
                values: new object[] { 1, "artimmerman@landstede.nl", "$2a$11$A1PAL2tcek6yMqg8VVVzauteuOFGnD1S4DoqlPP/Hf9ulBwfJTS8y", null, "Arjan Timmerman", "$2a$11$FIhli04K3CDnTp4ObcYIb.", "Africa/Abidjan", 4 });
        }
    }
}
