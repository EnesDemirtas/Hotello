using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotello.API.Migrations;

/// <inheritdoc />
public partial class AddDefaultRoles : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "8f0fd3d5-ed9b-4fc5-9fcd-6220f46bef66", null, "Admin", "ADMIN" },
                { "b8baaf49-611a-42d1-ae40-b67a77b45766", null, "User", "USER" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "8f0fd3d5-ed9b-4fc5-9fcd-6220f46bef66");

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "b8baaf49-611a-42d1-ae40-b67a77b45766");
    }
}
