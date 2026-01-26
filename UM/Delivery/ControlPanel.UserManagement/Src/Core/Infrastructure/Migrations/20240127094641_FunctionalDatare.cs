using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class FunctionalDatare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformationJob_LK_JobDegree_JobDegreeId",
                table: "InformationJob");

            migrationBuilder.AlterColumn<Guid>(
                name: "JobDegreeId",
                table: "InformationJob",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_InformationJob_LK_JobDegree_JobDegreeId",
                table: "InformationJob",
                column: "JobDegreeId",
                principalTable: "LK_JobDegree",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformationJob_LK_JobDegree_JobDegreeId",
                table: "InformationJob");

            migrationBuilder.AlterColumn<Guid>(
                name: "JobDegreeId",
                table: "InformationJob",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InformationJob_LK_JobDegree_JobDegreeId",
                table: "InformationJob",
                column: "JobDegreeId",
                principalTable: "LK_JobDegree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
