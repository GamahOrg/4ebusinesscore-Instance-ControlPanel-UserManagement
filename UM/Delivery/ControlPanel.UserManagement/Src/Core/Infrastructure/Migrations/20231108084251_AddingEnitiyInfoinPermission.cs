using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class AddingEnitiyInfoinPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_CreatedBy",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuickAccess_Users_UserId",
                table: "QuickAccess");

            migrationBuilder.DropIndex(
                name: "IX_QuickAccess_WorkSpaceCardId_UserId",
                table: "QuickAccess");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_CreatedBy",
                table: "Permissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "QuickAccess",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuickAccess_WorkSpaceCardId_UserId",
                table: "QuickAccess",
                columns: new[] { "WorkSpaceCardId", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuickAccess_Users_UserId",
                table: "QuickAccess",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuickAccess_Users_UserId",
                table: "QuickAccess");

            migrationBuilder.DropIndex(
                name: "IX_QuickAccess_WorkSpaceCardId_UserId",
                table: "QuickAccess");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Permissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "QuickAccess",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuickAccess_WorkSpaceCardId_UserId",
                table: "QuickAccess",
                columns: new[] { "WorkSpaceCardId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedBy",
                table: "Permissions",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_CreatedBy",
                table: "Permissions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuickAccess_Users_UserId",
                table: "QuickAccess",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
