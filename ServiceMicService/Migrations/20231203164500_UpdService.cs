using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceMicService.Migrations
{
    /// <inheritdoc />
    public partial class UpdService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "services");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "services");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "services",
                newName: "DescriptionService");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionService",
                table: "services",
                newName: "Time");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
