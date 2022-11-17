using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotProperty_PropertyAPI.Migrations
{
    public partial class MakeHomeAndLaptopDbSameNameLocal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 12, 57, 22, 540, DateTimeKind.Local).AddTicks(1913));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 12, 57, 22, 540, DateTimeKind.Local).AddTicks(1918));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 12, 57, 22, 540, DateTimeKind.Local).AddTicks(1921));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 12, 21, 33, 823, DateTimeKind.Local).AddTicks(1589));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 12, 21, 33, 823, DateTimeKind.Local).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 12, 21, 33, 823, DateTimeKind.Local).AddTicks(1603));
        }
    }
}
