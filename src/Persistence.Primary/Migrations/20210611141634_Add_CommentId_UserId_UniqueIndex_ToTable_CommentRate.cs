using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class Add_CommentId_UserId_UniqueIndex_ToTable_CommentRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CommentRate_CommentId_UserId",
                table: "CommentRate",
                columns: new[] { "CommentId", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentRate_CommentId_UserId",
                table: "CommentRate");
        }
    }
}
