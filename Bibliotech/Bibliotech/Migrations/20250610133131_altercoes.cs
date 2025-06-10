using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bibliotech.Migrations
{
    /// <inheritdoc />
    public partial class altercoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2ec0b6-ca92-4643-b66d-af3ed5548d28");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "def89a59-6b11-48f3-9c5f-0a13ab2495b1", "353e7237-2571-4af1-a743-f5b3c94486da" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "def89a59-6b11-48f3-9c5f-0a13ab2495b1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "353e7237-2571-4af1-a743-f5b3c94486da");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04a1b200-b5a5-4405-886e-f6e938757012", null, "Leitor", "LEITOR" },
                    { "5f0162b6-e321-404d-a2d4-7693cfee64ea", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7dae8500-429e-4bfd-a438-a5a2afbe7d4f", 0, "8a4cacbe-dd9e-41a5-a68e-23f13ccd5ead", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAECKaG6NFOEN4LH7Hc+nenTNsrR9I/HB5MaOea6txzQzjTgkmimgc2CEKrvHGV5LmXw==", null, false, "51d55008-4380-403f-a523-5e1a88ad68a6", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5f0162b6-e321-404d-a2d4-7693cfee64ea", "7dae8500-429e-4bfd-a438-a5a2afbe7d4f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04a1b200-b5a5-4405-886e-f6e938757012");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5f0162b6-e321-404d-a2d4-7693cfee64ea", "7dae8500-429e-4bfd-a438-a5a2afbe7d4f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f0162b6-e321-404d-a2d4-7693cfee64ea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dae8500-429e-4bfd-a438-a5a2afbe7d4f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "def89a59-6b11-48f3-9c5f-0a13ab2495b1", null, "Admin", "ADMIN" },
                    { "fa2ec0b6-ca92-4643-b66d-af3ed5548d28", null, "Leitor", "LEITOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "353e7237-2571-4af1-a743-f5b3c94486da", 0, "c846ce2e-7eeb-47c5-8419-122becd6fd21", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEEzgRG0rq2C03taDucvM/n75mgHKLvoABjqajL/dg6Pn9Yp8lvcHysp56rB1ZWup4Q==", null, false, "c6f2b968-a4fe-489f-b8aa-856e54c3d75c", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "def89a59-6b11-48f3-9c5f-0a13ab2495b1", "353e7237-2571-4af1-a743-f5b3c94486da" });
        }
    }
}
