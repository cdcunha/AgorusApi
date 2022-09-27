using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgorusRepository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileModelId);
                });

            migrationBuilder.CreateTable(
                name: "FileHistories",
                columns: table => new
                {
                    FileHistoryModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Length = table.Column<long>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileContent = table.Column<byte[]>(type: "BLOB", nullable: false),
                    FileModelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileHistories", x => x.FileHistoryModelId);
                    table.ForeignKey(
                        name: "FK_FileHistories_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "FileModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idxDate",
                table: "FileHistories",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_FileHistories_FileModelId",
                table: "FileHistories",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "idxName",
                table: "Files",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileHistories");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
