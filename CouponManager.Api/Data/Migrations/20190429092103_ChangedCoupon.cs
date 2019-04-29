using Microsoft.EntityFrameworkCore.Migrations;

namespace CouponManager.Api.Data.Migrations
{
    public partial class ChangedCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CompanyId_Code",
                table: "Coupons",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_CompanyId_Code",
                table: "Coupons");
        }
    }
}
