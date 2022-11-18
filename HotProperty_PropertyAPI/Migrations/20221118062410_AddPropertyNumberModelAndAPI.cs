using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotProperty_PropertyAPI.Migrations
{
    public partial class AddPropertyNumberModelAndAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyNumbers",
                columns: table => new
                {
                    PropertyNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyNumbers", x => x.PropertyNo);
                });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 18, 17, 24, 9, 944, DateTimeKind.Local).AddTicks(5773));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 18, 17, 24, 9, 944, DateTimeKind.Local).AddTicks(5779));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 18, 17, 24, 9, 944, DateTimeKind.Local).AddTicks(5784));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyNumbers");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 22, 41, 20, 832, DateTimeKind.Local).AddTicks(2221));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 22, 41, 20, 832, DateTimeKind.Local).AddTicks(2227));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 22, 41, 20, 832, DateTimeKind.Local).AddTicks(2232));
        }
    }
}
