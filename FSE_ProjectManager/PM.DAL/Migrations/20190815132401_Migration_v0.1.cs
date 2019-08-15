using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.DAL.Migrations
{
    public partial class Migration_v01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsSuspended = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ParentTaskId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_ParentTasks_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalTable: "ParentTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ParentTasks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ParentTask1" },
                    { 2, "parentTask2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmployeeId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "1", "FirstName1", "LastName1" },
                    { 2, "2", "FirstName2", "LastName2" },
                    { 3, "3", "FirstName3", "LastName3" },
                    { 4, "4", "FirstName4", "LastName4" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "EndDate", "IsSuspended", "ManagerId", "Name", "Priority", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Project1", 1, new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Project2", 5, new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Project3", 2, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Project4", 0, new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "EndDate", "Name", "OwnerId", "ParentTaskId", "Priority", "ProjectId", "StartDate", "StatusId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task1", 2, 1, 1, 1, new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task2", 2, 1, 2, 2, new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task3", 3, 1, 3, 3, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task4", 4, 1, 4, 4, new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ParentTaskId",
                table: "Tasks",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "ParentTasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
