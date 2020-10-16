using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPM.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    VatNumber = table.Column<string>(maxLength: 250, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    BillableTime = table.Column<DateTime>(nullable: true),
                    CostPerHour = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    TaskCost = table.Column<decimal>(type: "decimal(16,4)", nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Task_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Name", "VatNumber", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Sample Address A", "Sample Costumer A", "500500500", "4470" },
                    { 2, "Sample Address B", "Sample Costumer B", "500500501", "4471" },
                    { 3, "Sample Address C", "Sample Costumer C", "500500502", "4472" },
                    { 4, "Sample Address D", "Sample Costumer D", "500500503", "4473" },
                    { 5, "Sample Address E", "Sample Costumer E", "500500504", "4474" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CreateDate", "CustomerId", "DeliveryDate", "Description", "EndDate", "Title" },
                values: new object[] { 1, new DateTime(2020, 10, 15, 0, 35, 28, 602, DateTimeKind.Local).AddTicks(7991), 1, new DateTime(2020, 10, 27, 0, 35, 28, 604, DateTimeKind.Local).AddTicks(8571), "Sample Project Description A1", null, "Sample project A1" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CreateDate", "CustomerId", "DeliveryDate", "Description", "EndDate", "Title" },
                values: new object[] { 3, new DateTime(2020, 10, 7, 0, 35, 28, 605, DateTimeKind.Local).AddTicks(225), 2, new DateTime(2020, 11, 10, 0, 35, 28, 605, DateTimeKind.Local).AddTicks(228), "Sample Project Description B3", null, "Sample project B3" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CreateDate", "CustomerId", "DeliveryDate", "Description", "EndDate", "Title" },
                values: new object[] { 2, new DateTime(2020, 10, 15, 0, 35, 28, 605, DateTimeKind.Local).AddTicks(174), 4, new DateTime(2020, 12, 6, 0, 35, 28, 605, DateTimeKind.Local).AddTicks(201), "Sample Project Description D2", null, "Sample project D2" });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskId", "BillableTime", "CostPerHour", "CreateDate", "Description", "EndDate", "ProjectId", "TaskCost", "Title" },
                values: new object[] { 1, new DateTime(2020, 10, 16, 23, 45, 28, 605, DateTimeKind.Local).AddTicks(1214), 4.215m, new DateTime(2020, 10, 15, 0, 35, 28, 605, DateTimeKind.Local).AddTicks(2643), "Task Project 1", null, 1, null, "Task Project 1" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ProjectId",
                table: "Task",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
