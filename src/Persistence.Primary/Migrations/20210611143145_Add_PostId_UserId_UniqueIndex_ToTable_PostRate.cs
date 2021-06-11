using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class Add_PostId_UserId_UniqueIndex_ToTable_PostRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PostRate_PostId_UserId",
                table: "PostRate",
                columns: new[] { "PostId", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostRate_PostId_UserId",
                table: "PostRate");
        }
    }
}
