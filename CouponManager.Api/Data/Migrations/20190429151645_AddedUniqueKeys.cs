using Microsoft.EntityFrameworkCore.Migrations;

namespace CouponManager.Api.Data.Migrations
{
    public partial class AddedUniqueKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Domains",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Domains_Url",
                table: "Domains",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Url",
                table: "Companies",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Domains_Url",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Url",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Domains",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
