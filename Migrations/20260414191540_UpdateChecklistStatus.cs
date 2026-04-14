using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChecklistStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "UserChecklistItemStatus",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserChecklistItemStatus",
                newName: "IsCompleted");
        }
    }
}
