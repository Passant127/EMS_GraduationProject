using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class FixExtraPropertyDictionary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "Discriminator",
            //    table: "AbpUsers",
            //    type: "nvarchar(13)",
            //    maxLength: 13,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(8)",
            //    oldMaxLength: 8);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                    name: "Discriminator",
                    table: "AbpUsers",
                    type: "nvarchar(64)",
                    nullable: false,
                    defaultValue: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AbpUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }
    }
}
