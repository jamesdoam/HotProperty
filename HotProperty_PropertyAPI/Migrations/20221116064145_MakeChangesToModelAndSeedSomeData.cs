using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotProperty_PropertyAPI.Migrations
{
    public partial class MakeChangesToModelAndSeedSomeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Area", "AskingPrice", "CreatedDate", "ImageUrl", "Name", "NoBedroom", "NoToilet", "PostCode", "State", "Suburb", "UpdatedDate" },
                values: new object[] { 1, 591, 980000, new DateTime(2022, 11, 16, 17, 41, 44, 884, DateTimeKind.Local).AddTicks(8609), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "11 James St", 3, 4, "3084", "VIC", "Heidelberg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Area", "AskingPrice", "CreatedDate", "ImageUrl", "Name", "NoBedroom", "NoToilet", "PostCode", "State", "Suburb", "UpdatedDate" },
                values: new object[] { 2, 750, 1080000, new DateTime(2022, 11, 16, 17, 41, 44, 884, DateTimeKind.Local).AddTicks(8614), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "16 Lily Crt", 4, 2, "2011", "NSW", "Frankston", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Area", "AskingPrice", "CreatedDate", "ImageUrl", "Name", "NoBedroom", "NoToilet", "PostCode", "State", "Suburb", "UpdatedDate" },
                values: new object[] { 3, 720, 688000, new DateTime(2022, 11, 16, 17, 41, 44, 884, DateTimeKind.Local).AddTicks(8617), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "177 Wonderwomen Prd", 4, 2, "4011", "QLD", "Hans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Properties");
        }
    }
}
