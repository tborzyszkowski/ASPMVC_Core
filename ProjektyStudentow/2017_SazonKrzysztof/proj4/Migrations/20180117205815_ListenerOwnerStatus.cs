using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace proj4.Migrations
{
    public partial class ListenerOwnerStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "listener",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "listener");
        }
    }
}
