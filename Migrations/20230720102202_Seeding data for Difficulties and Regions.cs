using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebSampleApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5c9ce373-4a0e-43ba-9a64-9a1a1c3f84fb"), "Hard" },
                    { new Guid("74e89cfd-3444-4035-a124-94c7758ad2ad"), "Medium" },
                    { new Guid("bb888a63-824e-47d9-9b6d-e0ba7a6fc477"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("296575e8-24a2-407b-a887-57ab7beb9562"), "BOB R", "Bay of Bengal", null },
                    { new Guid("fbb75597-89c6-4510-9fc9-471c37ae0bd7"), "BOB R", "Bank of Bengal", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5c9ce373-4a0e-43ba-9a64-9a1a1c3f84fb"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("74e89cfd-3444-4035-a124-94c7758ad2ad"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("bb888a63-824e-47d9-9b6d-e0ba7a6fc477"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("296575e8-24a2-407b-a887-57ab7beb9562"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fbb75597-89c6-4510-9fc9-471c37ae0bd7"));
        }
    }
}
