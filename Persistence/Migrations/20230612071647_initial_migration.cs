﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: false),
                    Woreda = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    SubCity = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    ServiceDescription = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfileSpeciality",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialitiesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfileSpeciality", x => new { x.DoctorsId, x.SpecialitiesId });
                    table.ForeignKey(
                        name: "FK_DoctorProfileSpeciality_Specialities_SpecialitiesId",
                        column: x => x.SpecialitiesId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDay = table.Column<string>(type: "text", nullable: false),
                    EndDay = table.Column<string>(type: "text", nullable: false),
                    Opening = table.Column<string>(type: "text", nullable: false),
                    Closing = table.Column<string>(type: "text", nullable: false),
                    TwentyFourHours = table.Column<bool>(type: "boolean", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionAvailabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionProfileServices",
                columns: table => new
                {
                    InstitutionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionProfileServices", x => new { x.InstitutionsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_InstitutionProfileServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstitutioProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionName = table.Column<string>(type: "text", nullable: false),
                    BranchName = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    EstablishedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogoId = table.Column<string>(type: "text", nullable: false),
                    BannerId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutioProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstitutioProfiles_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    InstitutionProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    DoctorProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    LogoId = table.Column<Guid>(type: "uuid", nullable: true),
                    BannerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_InstitutioProfiles_InstitutionProfileId",
                        column: x => x.InstitutionProfileId,
                        principalTable: "InstitutioProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    About = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhotoId = table.Column<string>(type: "text", nullable: false),
                    CareerStartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    MainInstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_InstitutioProfiles_MainInstitutionId",
                        column: x => x.MainInstitutionId,
                        principalTable: "InstitutioProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<string>(type: "text", nullable: false),
                    EndTime = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_InstitutioProfiles_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "InstitutioProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfileInstitutionProfile",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfileInstitutionProfile", x => new { x.DoctorsId, x.InstitutionsId });
                    table.ForeignKey(
                        name: "FK_DoctorProfileInstitutionProfile_DoctorProfiles_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorProfileInstitutionProfile_InstitutioProfiles_Institut~",
                        column: x => x.InstitutionsId,
                        principalTable: "InstitutioProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationInstitution = table.Column<string>(type: "text", nullable: false),
                    StartYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GraduationYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionLogoId = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Educations_Photos_InstitutionLogoId",
                        column: x => x.InstitutionLogoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiences_InstitutioProfiles_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "InstitutioProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_DoctorId",
                table: "DoctorAvailabilities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_InstitutionId",
                table: "DoctorAvailabilities",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_SpecialityId",
                table: "DoctorAvailabilities",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfileInstitutionProfile_InstitutionsId",
                table: "DoctorProfileInstitutionProfile",
                column: "InstitutionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_MainInstitutionId",
                table: "DoctorProfiles",
                column: "MainInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_PhotoId",
                table: "DoctorProfiles",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfileSpeciality_SpecialitiesId",
                table: "DoctorProfileSpeciality",
                column: "SpecialitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_DoctorId",
                table: "Educations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_InstitutionLogoId",
                table: "Educations",
                column: "InstitutionLogoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_DoctorId",
                table: "Experiences",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_InstitutionId",
                table: "Experiences",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionAvailabilities_InstitutionId",
                table: "InstitutionAvailabilities",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionProfileServices_ServicesId",
                table: "InstitutionProfileServices",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutioProfiles_AddressId",
                table: "InstitutioProfiles",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstitutioProfiles_BannerId",
                table: "InstitutioProfiles",
                column: "BannerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstitutioProfiles_LogoId",
                table: "InstitutioProfiles",
                column: "LogoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_InstitutionProfileId",
                table: "Photos",
                column: "InstitutionProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceName",
                table: "Services",
                column: "ServiceName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfileSpeciality_DoctorProfiles_DoctorsId",
                table: "DoctorProfileSpeciality",
                column: "DoctorsId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionAvailabilities_InstitutioProfiles_InstitutionId",
                table: "InstitutionAvailabilities",
                column: "InstitutionId",
                principalTable: "InstitutioProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionProfileServices_InstitutioProfiles_InstitutionsId",
                table: "InstitutionProfileServices",
                column: "InstitutionsId",
                principalTable: "InstitutioProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutioProfiles_Photos_BannerId",
                table: "InstitutioProfiles",
                column: "BannerId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutioProfiles_Photos_LogoId",
                table: "InstitutioProfiles",
                column: "LogoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_InstitutioProfiles_InstitutionProfileId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "DoctorAvailabilities");

            migrationBuilder.DropTable(
                name: "DoctorProfileInstitutionProfile");

            migrationBuilder.DropTable(
                name: "DoctorProfileSpeciality");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "InstitutionAvailabilities");

            migrationBuilder.DropTable(
                name: "InstitutionProfileServices");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "DoctorProfiles");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "InstitutioProfiles");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}