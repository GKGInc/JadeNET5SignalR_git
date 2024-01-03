using Microsoft.EntityFrameworkCore.Migrations;

namespace JadeNET5SignalR.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachinesDataView",
                columns: table => new
                {
                    Workcenter = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MachineState = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrder = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OpStep = table.Column<string>(type: "int", nullable: false),
                    OID = table.Column<string>(type: "int", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartsRequired = table.Column<string>(type: "int", nullable: false),
                    CycleCount = table.Column<string>(type: "int", nullable: false),
                    PartsMade = table.Column<string>(type: "int", nullable: false),
                    TotalPartsProduced = table.Column<string>(type: "int", nullable: false),
                    FinalTotalPartsProduced = table.Column<string>(type: "int", nullable: false),
                    CompletedQty = table.Column<string>(type: "int", nullable: false),
                    PartsToGo = table.Column<string>(type: "int", nullable: false),
                    LastGoodPartsId = table.Column<string>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workcenter", x => x.Workcenter);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cityname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "MachinesDataView");

            //migrationBuilder.DropTable(
            //    name: "Employee");

            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
