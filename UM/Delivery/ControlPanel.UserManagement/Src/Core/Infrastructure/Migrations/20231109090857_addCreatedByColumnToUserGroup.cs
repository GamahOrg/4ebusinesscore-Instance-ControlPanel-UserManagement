using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class addCreatedByColumnToUserGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "UserGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "UserGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_CreatedBy",
                table: "UserGroups",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_CreatedBy",
                table: "UserGroups",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_CreatedBy",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_CreatedBy",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "UserGroups");
        }
    }
}
