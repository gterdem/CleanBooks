using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanBooks.Infrastructure.Persistence.Migrations
{
    public partial class Renamed_Volume_To_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "VolumeInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GApiVolumeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolumeInfo_BookId",
                table: "VolumeInfo",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeInfo_Books_BookId",
                table: "VolumeInfo",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeInfo_Books_BookId",
                table: "VolumeInfo");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropIndex(
                name: "IX_VolumeInfo_BookId",
                table: "VolumeInfo");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "VolumeInfo");

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolumeInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GApiVolumeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volumes_VolumeInfo_VolumeInfoId",
                        column: x => x.VolumeInfoId,
                        principalTable: "VolumeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_VolumeInfoId",
                table: "Volumes",
                column: "VolumeInfoId");
        }
    }
}
