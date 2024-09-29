using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Test_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_Shelter_ShelterId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Payment_DonationId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Shelter_ShelterId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_User_UserId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Shelter_ShelterId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Shelter_UserId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_Event_EventId",
                table: "UserEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_User_UserId",
                table: "UserEvent");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shelter",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DonationId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Shelter_UserId",
                table: "Shelter",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_DonationId",
                table: "Payment",
                column: "DonationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_Shelter_ShelterId",
                table: "Adoption",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Shelter_ShelterId",
                table: "Donation",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_User_UserId",
                table: "Donation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Shelter_ShelterId",
                table: "Event",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Donation_DonationId",
                table: "Payment",
                column: "DonationId",
                principalTable: "Donation",
                principalColumn: "DonationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelter_User_UserId",
                table: "Shelter",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_Event_EventId",
                table: "UserEvent",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_User_UserId",
                table: "UserEvent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_Shelter_ShelterId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Shelter_ShelterId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_User_UserId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Shelter_ShelterId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Donation_DonationId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelter_User_UserId",
                table: "Shelter");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_Event_EventId",
                table: "UserEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_User_UserId",
                table: "UserEvent");

            migrationBuilder.DropIndex(
                name: "IX_Shelter_UserId",
                table: "Shelter");

            migrationBuilder.DropIndex(
                name: "IX_Payment_DonationId",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shelter",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DonationId",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_Shelter_ShelterId",
                table: "Adoption",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Payment_DonationId",
                table: "Donation",
                column: "DonationId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Shelter_ShelterId",
                table: "Donation",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_User_UserId",
                table: "Donation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Shelter_ShelterId",
                table: "Event",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Shelter_UserId",
                table: "User",
                column: "UserId",
                principalTable: "Shelter",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_Event_EventId",
                table: "UserEvent",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_User_UserId",
                table: "UserEvent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
