using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UniqueConstraintOnFlirtsForStudentAndCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropIndex(
            //     name: "IX_Flirts_CompanyId",
            //     table: "Flirts");

            migrationBuilder.CreateIndex(
                name: "IX_Flirts_CompanyId_StudentId",
                table: "Flirts",
                columns: new[] { "CompanyId", "StudentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Flirts_CompanyId_StudentId",
                table: "Flirts");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Flirts_CompanyId",
            //     table: "Flirts",
            //     column: "CompanyId");
        }
    }
}
