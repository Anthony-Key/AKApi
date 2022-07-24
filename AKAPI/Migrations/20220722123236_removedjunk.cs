using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKAPI.Migrations
{
    public partial class removedjunk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "NationalParks");

            migrationBuilder.DropColumn(
                name: "Established",
                table: "NationalParks");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "NationalParks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NationalParks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Established",
                table: "NationalParks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "NationalParks",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
