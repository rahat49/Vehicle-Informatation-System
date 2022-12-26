using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIMS.Migrations
{
    public partial class CreatenewAcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Damage",
                table: "AccidentCases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Damage",
                table: "AccidentCases");
        }
    }
}
