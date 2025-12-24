using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace firstProgram.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_feesDetails",
                table: "feesDetails");

            migrationBuilder.RenameTable(
                name: "feesDetails",
                newName: "FeesDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeesDetails",
                table: "FeesDetails",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FeesDetails",
                table: "FeesDetails");

            migrationBuilder.RenameTable(
                name: "FeesDetails",
                newName: "feesDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_feesDetails",
                table: "feesDetails",
                column: "Id");
        }
    }
}
