using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StammPhoenix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventPublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "public",
                table: "events",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "public",
                table: "events");
        }
    }
}
