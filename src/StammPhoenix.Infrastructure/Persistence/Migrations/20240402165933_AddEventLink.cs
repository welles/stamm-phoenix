using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "events",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                table: "events");
        }
    }
}
