using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRA.Migrations
{
    public partial class Members : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bf7d6e8-f6ef-4241-8d9b-ee8f9242e652");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c28b82c-a74f-4d03-9f76-3b4cba246967");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccaeec72-9d3d-4890-af25-d59cf20f27a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f04717e8-02b3-4b85-ab2f-ea78d0183e6f");

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<Guid>(nullable: false, defaultValueSql: "NewId()"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Ssn = table.Column<string>(maxLength: 300, nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ccaeec72-9d3d-4890-af25-d59cf20f27a2", "8651d1ec-4faf-43db-b292-459b377cbe9d", "SuperAdmin", "SUPERADMIN" },
                    { "f04717e8-02b3-4b85-ab2f-ea78d0183e6f", "e98b23f6-6a1c-4f78-925e-f5e81a239eaf", "Admin", "ADMIN" },
                    { "1bf7d6e8-f6ef-4241-8d9b-ee8f9242e652", "ff83eeae-a08b-4463-bd0e-27e239029150", "Group", "GROUP" },
                    { "7c28b82c-a74f-4d03-9f76-3b4cba246967", "0248fcbd-b8c0-47d2-a6c2-efa9c10f515f", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "ExtraInfo", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "Url" },
                values: new object[,]
                {
                    { 14, null, null, null, null, false, null, null, "Sample Video 2", "https://player.vimeo.com/video/112866269/" },
                    { 13, null, null, null, null, false, null, null, "Sample Video 1", "https://player.vimeo.com/video/347119375/" },
                    { 12, null, null, null, null, false, null, null, "Sample Video 4", "https://player.vimeo.com/video/444571537/" },
                    { 11, null, null, null, null, false, null, null, "Sample Video 3", "https://player.vimeo.com/video/6370469/" },
                    { 10, null, null, null, null, false, null, null, "Sample Video 2", "https://player.vimeo.com/video/112866269/" },
                    { 9, null, null, null, null, false, null, null, "Sample Video 1", "https://player.vimeo.com/video/347119375/" },
                    { 8, null, null, null, null, false, null, null, "Sample Video 4", "https://player.vimeo.com/video/444571537/" },
                    { 6, null, null, null, null, false, null, null, "Sample Video 2", "https://player.vimeo.com/video/112866269/" },
                    { 15, null, null, null, null, false, null, null, "Sample Video 3", "https://player.vimeo.com/video/6370469/" },
                    { 5, null, null, null, null, false, null, null, "Sample Video 1", "https://player.vimeo.com/video/347119375/" },
                    { 4, null, null, null, null, false, null, null, "Sample Video 4", "https://player.vimeo.com/video/444571537/" },
                    { 3, null, null, null, null, false, null, null, "Sample Video 3", "https://player.vimeo.com/video/6370469/" },
                    { 2, null, null, null, null, false, null, null, "Sample Video 2", "https://player.vimeo.com/video/112866269/" },
                    { 1, null, null, null, null, false, null, null, "Sample Video 1", "https://player.vimeo.com/video/347119375/" },
                    { 7, null, null, null, null, false, null, null, "Sample Video 3", "https://player.vimeo.com/video/6370469/" },
                    { 16, null, null, null, null, false, null, null, "Sample Video 4", "https://player.vimeo.com/video/444571537/" }
                });
        }
    }
}
