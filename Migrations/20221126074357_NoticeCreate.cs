using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIMS.Migrations
{
    public partial class NoticeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVV",
                table: "Renewapplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardNumber",
                table: "Renewapplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CardOwnerName",
                table: "Renewapplies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpiryDate",
                table: "Renewapplies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Fees",
                table: "Renewapplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Renewapplies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropColumn(
                name: "CVV",
                table: "Renewapplies");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Renewapplies");

            migrationBuilder.DropColumn(
                name: "CardOwnerName",
                table: "Renewapplies");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Renewapplies");

            migrationBuilder.DropColumn(
                name: "Fees",
                table: "Renewapplies");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Renewapplies");
        }
    }
}
