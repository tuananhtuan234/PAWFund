using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class fixCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Adoption_AdoptionId",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoption",
                table: "Adoption");

            migrationBuilder.RenameTable(
                name: "Adoption",
                newName: "Adoptions");

            migrationBuilder.RenameIndex(
                name: "IX_Adoption_UserId",
                table: "Adoptions",
                newName: "IX_Adoptions_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions",
                column: "AdoptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_User_UserId",
                table: "Adoptions",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Adoptions_AdoptionId",
                table: "Pet",
                column: "AdoptionId",
                principalTable: "Adoptions",
                principalColumn: "AdoptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_User_UserId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Adoptions_AdoptionId",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions");

            migrationBuilder.RenameTable(
                name: "Adoptions",
                newName: "Adoption");

            migrationBuilder.RenameIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoption",
                newName: "IX_Adoption_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoption",
                table: "Adoption",
                column: "AdoptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Adoption_AdoptionId",
                table: "Pet",
                column: "AdoptionId",
                principalTable: "Adoption",
                principalColumn: "AdoptionId");
        }
    }
}
