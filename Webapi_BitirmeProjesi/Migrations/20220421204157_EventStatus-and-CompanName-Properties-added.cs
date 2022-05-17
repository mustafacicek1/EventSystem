using Microsoft.EntityFrameworkCore.Migrations;

namespace Webapi_BitirmeProjesi.Migrations
{
    public partial class EventStatusandCompanNamePropertiesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EventStatus",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Companies",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventStatus",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Companies");
        }
    }
}
