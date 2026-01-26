using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class addRoleCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleCategoryId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LK_RoleCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_RoleCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_RoleCategory_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleCategoryId",
                table: "Roles",
                column: "RoleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LK_RoleCategory_CreatedBy",
                table: "LK_RoleCategory",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_LK_RoleCategory_RoleCategoryId",
                table: "Roles",
                column: "RoleCategoryId",
                principalTable: "LK_RoleCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_LK_RoleCategory_RoleCategoryId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "LK_RoleCategory");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleCategoryId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleCategoryId",
                table: "Roles");
        }
    }
}
