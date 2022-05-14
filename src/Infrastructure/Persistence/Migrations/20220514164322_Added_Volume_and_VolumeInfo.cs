using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanBooks.Infrastructure.Persistence.Migrations
{
    public partial class Added_Volume_and_VolumeInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolumeInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: true),
                    MaturityRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowAnonLogging = table.Column<bool>(type: "bit", nullable: true),
                    ContentVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviewLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanonicalVolumeLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: true),
                    ComicsContent = table.Column<bool>(type: "bit", nullable: true),
                    MainCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SamplePageCount = table.Column<int>(type: "int", nullable: true),
                    PrintedPageCount = table.Column<int>(type: "int", nullable: true),
                    RatingsCount = table.Column<int>(type: "int", nullable: true),
                    PrintType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLinks_ExtraLarge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLinks_Large = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLinks_Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLinks_Small = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLinks_SmallThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLinks_Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingModes_Text = table.Column<bool>(type: "bit", nullable: true),
                    ReadingModes_Image = table.Column<bool>(type: "bit", nullable: true),
                    PanelizationSummary_ContainsEpubBubbles = table.Column<bool>(type: "bit", nullable: true),
                    PanelizationSummary_ContainsImageBubbles = table.Column<bool>(type: "bit", nullable: true),
                    PanelizationSummary_EpubBubbleVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelizationSummary_ImageBubbleVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimentions_Height = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimentions_Thickness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimentions_Width = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndustryIdentifier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolumeInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryIdentifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndustryIdentifier_VolumeInfo_VolumeInfoId",
                        column: x => x.VolumeInfoId,
                        principalTable: "VolumeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GApiVolumeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VolumeInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "IX_IndustryIdentifier_VolumeInfoId",
                table: "IndustryIdentifier",
                column: "VolumeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeInfo_Authors",
                table: "VolumeInfo",
                column: "Authors");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeInfo_Categories",
                table: "VolumeInfo",
                column: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeInfo_Id",
                table: "VolumeInfo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeInfo_Title",
                table: "VolumeInfo",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_VolumeInfoId",
                table: "Volumes",
                column: "VolumeInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndustryIdentifier");

            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.DropTable(
                name: "VolumeInfo");
        }
    }
}
