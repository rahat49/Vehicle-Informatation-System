using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIMS.Migrations
{
    public partial class ForthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccidentCases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccidentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccidentVCate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tpeople = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Injuredp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deathp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentCases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Renewapplies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<int>(type: "int", nullable: false),
                    Vehnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vcategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brandname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MadeYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChessisNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registrationdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxTNumber = table.Column<int>(type: "int", nullable: false),
                    TTExpirydate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renewapplies", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentCases");

            migrationBuilder.DropTable(
                name: "Renewapplies");
        }
    }
}
