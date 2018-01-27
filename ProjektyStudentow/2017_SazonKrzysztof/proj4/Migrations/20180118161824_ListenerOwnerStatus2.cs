using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace proj4.Migrations
{
    public partial class ListenerOwnerStatus2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "listener",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "listener");
        }
    }
}
