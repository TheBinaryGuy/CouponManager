using Microsoft.EntityFrameworkCore.Migrations;

namespace CouponManager.Api.Data.Migrations
{
    public partial class AddedUniqKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_CompanyId_Code",
                table: "Coupons");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CompanyId_DomainId_CategoryId_Code",
                table: "Coupons",
                columns: new[] { "CompanyId", "DomainId", "CategoryId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserName",
                table: "Companies",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_CompanyId_DomainId_CategoryId_Code",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserName",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CompanyId_Code",
                table: "Coupons",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }
    }
}
