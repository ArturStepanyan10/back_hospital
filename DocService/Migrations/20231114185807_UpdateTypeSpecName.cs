﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTypeSpecName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "doctors");

            migrationBuilder.AddColumn<string>(
                name: "SpecName",
                table: "doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecName",
                table: "doctors");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}