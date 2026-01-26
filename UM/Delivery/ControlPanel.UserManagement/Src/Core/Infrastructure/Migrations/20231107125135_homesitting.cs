using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class homesitting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SitelanguageId",
                table: "LanguageForm",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LookupId",
                table: "HomeSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LK_HomeSetting",
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
                    table.PrimaryKey("PK_LK_HomeSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_HomeSetting_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LK_LanguageForm",
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
                    table.PrimaryKey("PK_LK_LanguageForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_LanguageForm_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageForm_SitelanguageId",
                table: "LanguageForm",
                column: "SitelanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSettings_LookupId",
                table: "HomeSettings",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_LK_HomeSetting_CreatedBy",
                table: "LK_HomeSetting",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LK_LanguageForm_CreatedBy",
                table: "LK_LanguageForm",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSettings_LK_HomeSetting_LookupId",
                table: "HomeSettings",
                column: "LookupId",
                principalTable: "LK_HomeSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageForm_LK_LanguageForm_SitelanguageId",
                table: "LanguageForm",
                column: "SitelanguageId",
                principalTable: "LK_LanguageForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSettings_LK_HomeSetting_LookupId",
                table: "HomeSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageForm_LK_LanguageForm_SitelanguageId",
                table: "LanguageForm");

            migrationBuilder.DropTable(
                name: "LK_HomeSetting");

            migrationBuilder.DropTable(
                name: "LK_LanguageForm");

            migrationBuilder.DropIndex(
                name: "IX_LanguageForm_SitelanguageId",
                table: "LanguageForm");

            migrationBuilder.DropIndex(
                name: "IX_HomeSettings_LookupId",
                table: "HomeSettings");

            migrationBuilder.DropColumn(
                name: "SitelanguageId",
                table: "LanguageForm");

            migrationBuilder.DropColumn(
                name: "LookupId",
                table: "HomeSettings");
        }
    }
}
