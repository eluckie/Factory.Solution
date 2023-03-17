using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.Migrations
{
    /// <inheritdoc />
    public partial class AddEngineerProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EngineerId",
                table: "Repairs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Engineers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Engineers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Pronouns",
                table: "Engineers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_EngineerId",
                table: "Repairs",
                column: "EngineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Engineers_EngineerId",
                table: "Repairs",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Engineers_EngineerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_EngineerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "EngineerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Engineers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Engineers");

            migrationBuilder.DropColumn(
                name: "Pronouns",
                table: "Engineers");
        }
    }
}
