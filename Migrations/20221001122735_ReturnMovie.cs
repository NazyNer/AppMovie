using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMovie.Migrations
{
    public partial class ReturnMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReturnID",
                table: "RentalDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    ReturnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.ReturnID);
                    table.ForeignKey(
                        name: "FK_Return_Partner_PartnerID",
                        column: x => x.PartnerID,
                        principalTable: "Partner",
                        principalColumn: "PartnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnDetail",
                columns: table => new
                {
                    ReturnDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnID = table.Column<int>(type: "int", nullable: false),
                    RentalID = table.Column<int>(type: "int", nullable: true),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnDetail", x => x.ReturnDetailID);
                    table.ForeignKey(
                        name: "FK_ReturnDetail_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnDetail_Rental_RentalID",
                        column: x => x.RentalID,
                        principalTable: "Rental",
                        principalColumn: "RentalID");
                });

            migrationBuilder.CreateTable(
                name: "ReturnDetailTemp",
                columns: table => new
                {
                    ReturnDetailTempID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnDetailTemp", x => x.ReturnDetailTempID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetail_ReturnID",
                table: "RentalDetail",
                column: "ReturnID");

            migrationBuilder.CreateIndex(
                name: "IX_Return_PartnerID",
                table: "Return",
                column: "PartnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetail_MovieID",
                table: "ReturnDetail",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetail_RentalID",
                table: "ReturnDetail",
                column: "RentalID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetail_Return_ReturnID",
                table: "RentalDetail",
                column: "ReturnID",
                principalTable: "Return",
                principalColumn: "ReturnID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetail_Return_ReturnID",
                table: "RentalDetail");

            migrationBuilder.DropTable(
                name: "Return");

            migrationBuilder.DropTable(
                name: "ReturnDetail");

            migrationBuilder.DropTable(
                name: "ReturnDetailTemp");

            migrationBuilder.DropIndex(
                name: "IX_RentalDetail_ReturnID",
                table: "RentalDetail");

            migrationBuilder.DropColumn(
                name: "ReturnID",
                table: "RentalDetail");
        }
    }
}
