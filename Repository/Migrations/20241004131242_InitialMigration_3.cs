using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_Shelter_ShelterId",
                table: "Adoption");

            migrationBuilder.DropIndex(
                name: "IX_Adoption_ShelterId",
                table: "Adoption");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "Adoption");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetId",
                table: "Adoption",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Adoption",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Adoption_PetId",
                table: "Pet",
                column: "PetId",
                principalTable: "Adoption",
                principalColumn: "AdoptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Adoption_PetId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Adoption");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Adoption");

            migrationBuilder.AddColumn<string>(
                name: "ShelterId",
                table: "Adoption",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Adoption_ShelterId",
                table: "Adoption",
                column: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_Shelter_ShelterId",
                table: "Adoption",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId");
        }
    }
}
