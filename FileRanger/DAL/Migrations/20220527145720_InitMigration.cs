using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "snapshot",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    drive = table.Column<string>(type: "text", nullable: false),
                    hostname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_snapshot", x => x.rowid);
                });

            migrationBuilder.CreateTable(
                name: "file",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    fullPath = table.Column<string>(type: "text", nullable: false),
                    parentPath = table.Column<string>(type: "text", nullable: false),
                    extension = table.Column<string>(type: "text", nullable: false),
                    snapshotId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_file_snapshot_snapshotId",
                        column: x => x.snapshotId,
                        principalTable: "snapshot",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "folder",
                columns: table => new
                {
                    rowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    fullPath = table.Column<string>(type: "text", nullable: false),
                    parentPath = table.Column<string>(type: "text", nullable: false),
                    snapshotId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_folder", x => x.rowid);
                    table.ForeignKey(
                        name: "FK_folder_snapshot_snapshotId",
                        column: x => x.snapshotId,
                        principalTable: "snapshot",
                        principalColumn: "rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_snapshotId",
                table: "file",
                column: "snapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_folder_snapshotId",
                table: "folder",
                column: "snapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file");

            migrationBuilder.DropTable(
                name: "folder");

            migrationBuilder.DropTable(
                name: "snapshot");
        }
    }
}
