using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheduleService.Migrations
{
    /// <inheritdoc />
    public partial class NewColDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "shedules");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "shedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "shedules");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "shedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
