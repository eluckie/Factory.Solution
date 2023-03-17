using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.Migrations
{
    /// <inheritdoc />
    public partial class AddRepairProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Engineers_EngineerId",
                table: "Repairs");

            migrationBuilder.AlterColumn<int>(
                name: "EngineerId",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_MachineId",
                table: "Repairs",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Engineers_EngineerId",
                table: "Repairs",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Machines_MachineId",
                table: "Repairs",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Engineers_EngineerId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Machines_MachineId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_MachineId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Repairs");

            migrationBuilder.AlterColumn<int>(
                name: "EngineerId",
                table: "Repairs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Engineers_EngineerId",
                table: "Repairs",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId");
        }
    }
}
