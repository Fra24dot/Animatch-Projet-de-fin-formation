using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class energylevelenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnergyLevel",
                table: "Dogs",
                newName: "EnergyLevelEnum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnergyLevelEnum",
                table: "Dogs",
                newName: "EnergyLevel");
        }
    }
}
