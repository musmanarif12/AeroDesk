using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroDesk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationSentToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationSent",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationSent",
                table: "Flights");
        }
    }
}
