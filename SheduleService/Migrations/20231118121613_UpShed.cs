using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheduleService.Migrations
{
    /// <inheritdoc />
    public partial class UpShed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdmissionId",
                table: "shedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionId",
                table: "shedules");
        }
    }
}
