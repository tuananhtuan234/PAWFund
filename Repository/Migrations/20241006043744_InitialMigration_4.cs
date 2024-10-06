using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Adoption_PetId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Adoption");

            migrationBuilder.AddColumn<string>(
                name: "AdoptionId",
                table: "Pet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_AdoptionId",
                table: "Pet",
                column: "AdoptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Adoption_AdoptionId",
                table: "Pet",
                column: "AdoptionId",
                principalTable: "Adoption",
                principalColumn: "AdoptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Adoption_AdoptionId",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_AdoptionId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "AdoptionId",
                table: "Pet");

            migrationBuilder.AddColumn<string>(
                name: "PetId",
                table: "Adoption",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Adoption_PetId",
                table: "Pet",
                column: "PetId",
                principalTable: "Adoption",
                principalColumn: "AdoptionId");
        }
    }
}
