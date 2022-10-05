using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMovie.Migrations
{
    public partial class ReturnDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetail_Return_ReturnID",
                table: "RentalDetail");

            migrationBuilder.DropIndex(
                name: "IX_RentalDetail_ReturnID",
                table: "RentalDetail");

            migrationBuilder.DropColumn(
                name: "ReturnID",
                table: "RentalDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetail_ReturnID",
                table: "ReturnDetail",
                column: "ReturnID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnDetail_Return_ReturnID",
                table: "ReturnDetail",
                column: "ReturnID",
                principalTable: "Return",
                principalColumn: "ReturnID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnDetail_Return_ReturnID",
                table: "ReturnDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReturnDetail_ReturnID",
                table: "ReturnDetail");

            migrationBuilder.AddColumn<int>(
                name: "ReturnID",
                table: "RentalDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetail_ReturnID",
                table: "RentalDetail",
                column: "ReturnID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetail_Return_ReturnID",
                table: "RentalDetail",
                column: "ReturnID",
                principalTable: "Return",
                principalColumn: "ReturnID");
        }
    }
}
