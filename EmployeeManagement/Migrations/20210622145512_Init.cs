using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Pesel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Surname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "BirthDate", "Name", "Pesel", "RegistrationNumber", "Sex", "Surname" },
                values: new object[] { 1, new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tony", "54092397111", "00000001", 0, "Stark" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "BirthDate", "Name", "Pesel", "RegistrationNumber", "Sex", "Surname" },
                values: new object[] { 2, new DateTime(1988, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bruce", "90102826611", "00000002", 0, "Banner" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "BirthDate", "Name", "Pesel", "RegistrationNumber", "Sex", "Surname" },
                values: new object[] { 3, new DateTime(1975, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sue", "38100948214", "00000003", 1, "Storm" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Pesel",
                table: "Employees",
                column: "Pesel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RegistrationNumber",
                table: "Employees",
                column: "RegistrationNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
