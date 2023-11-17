using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceMicService.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColCS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostService",
                table: "services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostService",
                table: "services");
        }
    }
}
