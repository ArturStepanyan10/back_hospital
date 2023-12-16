using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionService.Migrations
{
    /// <inheritdoc />
    public partial class UpdModelAdm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "admissions");

            migrationBuilder.DropColumn(
                name: "SheduleId",
                table: "admissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "admissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SheduleId",
                table: "admissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
