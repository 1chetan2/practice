using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace firstProgram.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeColumnToRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dept",
                table: "Registers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dept",
                table: "Registers");
        }
    }
}
