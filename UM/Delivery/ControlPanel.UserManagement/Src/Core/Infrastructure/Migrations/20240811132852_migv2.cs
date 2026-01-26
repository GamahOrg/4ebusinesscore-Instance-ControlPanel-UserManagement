using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatFormInfrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class migv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DomainUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DomainUrl",
                table: "Users");
        }
    }
}
