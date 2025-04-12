using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class changenameuser4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BOD",
                table: "AbpUsers",
                type: "date",
                nullable: true);

           
            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "AbpUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "AbpUsers",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BOD",
                table: "AbpUsers");

          
            migrationBuilder.DropColumn(
                name: "Height",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AbpUsers");
        }
    }
}
