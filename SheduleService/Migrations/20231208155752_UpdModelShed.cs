using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheduleService.Migrations
{
    /// <inheritdoc />
    public partial class UpdModelShed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "shedules");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "shedules",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "shedules",
                newName: "Time");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "shedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
