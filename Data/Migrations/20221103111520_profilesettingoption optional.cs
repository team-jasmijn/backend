using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class profilesettingoptionoptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilesettings_ProfilesettingsOptions_ProfileSettingOptionId",
                table: "Profilesettings");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileSettingOptionId",
                table: "Profilesettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilesettings_ProfilesettingsOptions_ProfileSettingOptionId",
                table: "Profilesettings",
                column: "ProfileSettingOptionId",
                principalTable: "ProfilesettingsOptions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilesettings_ProfilesettingsOptions_ProfileSettingOptionId",
                table: "Profilesettings");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileSettingOptionId",
                table: "Profilesettings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilesettings_ProfilesettingsOptions_ProfileSettingOptionId",
                table: "Profilesettings",
                column: "ProfileSettingOptionId",
                principalTable: "ProfilesettingsOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
