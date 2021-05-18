using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class Make_DeleteBehaviour_OfColumn_PostId_Of_PostRateTable_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRate_PostId",
                table: "PostRate");

            migrationBuilder.AddForeignKey(
                name: "FK_PostRate_PostId",
                table: "PostRate",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRate_PostId",
                table: "PostRate");

            migrationBuilder.AddForeignKey(
                name: "FK_PostRate_PostId",
                table: "PostRate",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
