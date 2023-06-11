using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class editedthemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstitutionAvailabilities_InstitutioProfiles_InstitutionId",
                table: "InstitutionAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_InstitutionAvailabilities_InstitutionId",
                table: "InstitutionAvailabilities");

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("6e1567ba-66a5-44f8-a578-2e013820f24f"));

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("f17f9988-1714-4220-b527-b5e26380e4ca"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionAvailabilityId",
                table: "InstitutioProfiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionId1",
                table: "InstitutionAvailabilities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "DateCreated", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("9a3b1dc0-6ade-4f96-bb0e-a744dd542fad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiltle 2" },
                    { new Guid("c1a39c7f-25b1-40c5-a20d-773e22c6c147"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "title" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstitutioProfiles_InstitutionAvailabilityId",
                table: "InstitutioProfiles",
                column: "InstitutionAvailabilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionAvailabilities_InstitutionId1",
                table: "InstitutionAvailabilities",
                column: "InstitutionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionAvailabilities_InstitutioProfiles_InstitutionId1",
                table: "InstitutionAvailabilities",
                column: "InstitutionId1",
                principalTable: "InstitutioProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutioProfiles_InstitutionAvailabilities_InstitutionAva~",
                table: "InstitutioProfiles",
                column: "InstitutionAvailabilityId",
                principalTable: "InstitutionAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstitutionAvailabilities_InstitutioProfiles_InstitutionId1",
                table: "InstitutionAvailabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_InstitutioProfiles_InstitutionAvailabilities_InstitutionAva~",
                table: "InstitutioProfiles");

            migrationBuilder.DropIndex(
                name: "IX_InstitutioProfiles_InstitutionAvailabilityId",
                table: "InstitutioProfiles");

            migrationBuilder.DropIndex(
                name: "IX_InstitutionAvailabilities_InstitutionId1",
                table: "InstitutionAvailabilities");

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("9a3b1dc0-6ade-4f96-bb0e-a744dd542fad"));

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("c1a39c7f-25b1-40c5-a20d-773e22c6c147"));

            migrationBuilder.DropColumn(
                name: "InstitutionAvailabilityId",
                table: "InstitutioProfiles");

            migrationBuilder.DropColumn(
                name: "InstitutionId1",
                table: "InstitutionAvailabilities");

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "DateCreated", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("6e1567ba-66a5-44f8-a578-2e013820f24f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiltle 2" },
                    { new Guid("f17f9988-1714-4220-b527-b5e26380e4ca"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Content", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "title" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionAvailabilities_InstitutionId",
                table: "InstitutionAvailabilities",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionAvailabilities_InstitutioProfiles_InstitutionId",
                table: "InstitutionAvailabilities",
                column: "InstitutionId",
                principalTable: "InstitutioProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
