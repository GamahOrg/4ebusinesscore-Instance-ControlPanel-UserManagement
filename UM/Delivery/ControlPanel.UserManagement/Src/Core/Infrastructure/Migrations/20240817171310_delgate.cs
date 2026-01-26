using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class delgate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_DelegeatedUserId",
                table: "UserDelegation");

            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "UserDelegation",
                newName: "IsApprovedTo");

            migrationBuilder.RenameColumn(
                name: "DelegeatedUserId",
                table: "UserDelegation",
                newName: "DelegeatedFromUserId");

            migrationBuilder.RenameColumn(
                name: "ApprovedNotes",
                table: "UserDelegation",
                newName: "ApprovedToNotes");

            migrationBuilder.RenameIndex(
                name: "IX_UserDelegation_DelegeatedUserId",
                table: "UserDelegation",
                newName: "IX_UserDelegation_DelegeatedFromUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedFromNotes",
                table: "UserDelegation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DelegeatedToUserId",
                table: "UserDelegation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "FromAdmin",
                table: "UserDelegation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedFrom",
                table: "UserDelegation",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_DelegeatedToUserId",
                table: "UserDelegation",
                column: "DelegeatedToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_DelegeatedFromUserId",
                table: "UserDelegation",
                column: "DelegeatedFromUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_DelegeatedToUserId",
                table: "UserDelegation",
                column: "DelegeatedToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_DelegeatedFromUserId",
                table: "UserDelegation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_DelegeatedToUserId",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_UserDelegation_DelegeatedToUserId",
                table: "UserDelegation");

            migrationBuilder.DropColumn(
                name: "ApprovedFromNotes",
                table: "UserDelegation");

            migrationBuilder.DropColumn(
                name: "DelegeatedToUserId",
                table: "UserDelegation");

            migrationBuilder.DropColumn(
                name: "FromAdmin",
                table: "UserDelegation");

            migrationBuilder.DropColumn(
                name: "IsApprovedFrom",
                table: "UserDelegation");

            migrationBuilder.RenameColumn(
                name: "IsApprovedTo",
                table: "UserDelegation",
                newName: "IsApproved");

            migrationBuilder.RenameColumn(
                name: "DelegeatedFromUserId",
                table: "UserDelegation",
                newName: "DelegeatedUserId");

            migrationBuilder.RenameColumn(
                name: "ApprovedToNotes",
                table: "UserDelegation",
                newName: "ApprovedNotes");

            migrationBuilder.RenameIndex(
                name: "IX_UserDelegation_DelegeatedFromUserId",
                table: "UserDelegation",
                newName: "IX_UserDelegation_DelegeatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_DelegeatedUserId",
                table: "UserDelegation",
                column: "DelegeatedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
