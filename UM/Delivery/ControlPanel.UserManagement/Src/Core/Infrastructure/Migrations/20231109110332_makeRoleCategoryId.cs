using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class makeRoleCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_LK_RoleCategory_RoleCategoryId",
                table: "Roles");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleCategoryId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_LK_RoleCategory_RoleCategoryId",
                table: "Roles",
                column: "RoleCategoryId",
                principalTable: "LK_RoleCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_LK_RoleCategory_RoleCategoryId",
                table: "Roles");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleCategoryId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_LK_RoleCategory_RoleCategoryId",
                table: "Roles",
                column: "RoleCategoryId",
                principalTable: "LK_RoleCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
