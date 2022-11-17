using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotProperty_PropertyAPI.Migrations
{
    public partial class backtolocalpc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 15, 50, 7, 192, DateTimeKind.Local).AddTicks(2658));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 15, 50, 7, 192, DateTimeKind.Local).AddTicks(2662));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 15, 50, 7, 192, DateTimeKind.Local).AddTicks(2666));
        }
    }
}
