using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Shelters",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Shelters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_Email",
                table: "Shelters",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shelters_Email",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Shelters");
        }
    }
}
