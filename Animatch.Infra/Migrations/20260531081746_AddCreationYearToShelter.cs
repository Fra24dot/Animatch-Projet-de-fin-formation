using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationYearToShelter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelterAgreementProof",
                table: "Shelters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShelterAgreementProof",
                table: "Shelters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
