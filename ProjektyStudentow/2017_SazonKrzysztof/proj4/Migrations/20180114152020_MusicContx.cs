using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace proj4.Migrations
{
    public partial class MusicContx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "listener",
                columns: table => new
                {
                    ListenerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listener", x => x.ListenerID);
                });

            migrationBuilder.CreateTable(
                name: "band",
                columns: table => new
                {
                    BandID = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    FormationDate = table.Column<DateTime>(nullable: true),
                    Genre = table.Column<int>(nullable: true),
                    ListenerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_band", x => x.BandID);
                    table.ForeignKey(
                        name: "FK_band_listener_ListenerID",
                        column: x => x.ListenerID,
                        principalTable: "listener",
                        principalColumn: "ListenerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "band_listener",
                columns: table => new
                {
                    BandID = table.Column<string>(nullable: false),
                    ListenerID = table.Column<int>(nullable: false),
                    Note = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_band_listener", x => new { x.BandID, x.ListenerID });
                    table.ForeignKey(
                        name: "FK_band_listener_band_BandID",
                        column: x => x.BandID,
                        principalTable: "band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_band_listener_listener_ListenerID",
                        column: x => x.ListenerID,
                        principalTable: "listener",
                        principalColumn: "ListenerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tour",
                columns: table => new
                {
                    TourID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BandID = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tour", x => x.TourID);
                    table.ForeignKey(
                        name: "FK_tour_band_BandID",
                        column: x => x.BandID,
                        principalTable: "band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_band_ListenerID",
                table: "band",
                column: "ListenerID");

            migrationBuilder.CreateIndex(
                name: "IX_band_listener_ListenerID",
                table: "band_listener",
                column: "ListenerID");

            migrationBuilder.CreateIndex(
                name: "IX_tour_BandID",
                table: "tour",
                column: "BandID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "band_listener");

            migrationBuilder.DropTable(
                name: "tour");

            migrationBuilder.DropTable(
                name: "band");

            migrationBuilder.DropTable(
                name: "listener");
        }
    }
}
