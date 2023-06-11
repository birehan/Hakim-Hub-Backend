using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class done : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("8ed738fd-b6c9-447e-907a-8db7f2f1b781"));

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("cff467e8-fd7d-41b4-bde4-e3d69082ebe6"));

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "DateCreated", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("85d07818-cf21-46b2-8b33-0cb085f8c705"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "title" },
                    { new Guid("f60f9820-6cb6-40bc-946b-11367488cbdf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiltle 2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("85d07818-cf21-46b2-8b33-0cb085f8c705"));

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("f60f9820-6cb6-40bc-946b-11367488cbdf"));

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "DateCreated", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("8ed738fd-b6c9-447e-907a-8db7f2f1b781"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiltle 2" },
                    { new Guid("cff467e8-fd7d-41b4-bde4-e3d69082ebe6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "title" }
                });
        }
    }
}
