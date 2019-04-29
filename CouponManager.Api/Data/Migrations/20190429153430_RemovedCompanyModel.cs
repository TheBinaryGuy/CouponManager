using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CouponManager.Api.Data.Migrations
{
    public partial class RemovedCompanyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CompanyId_DomainId_CategoryId_Code",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Coupons");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Domains",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Coupons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UserId",
                table: "Domains",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_UserId_DomainId_CategoryId_Code",
                table: "Coupons",
                columns: new[] { "UserId", "DomainId", "CategoryId", "Code" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_AspNetUsers_UserId",
                table: "Coupons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_AspNetUsers_UserId",
                table: "Domains",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_AspNetUsers_UserId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_AspNetUsers_UserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_UserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_UserId_DomainId_CategoryId_Code",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Domains",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Coupons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CompanyId_DomainId_CategoryId_Code",
                table: "Coupons",
                columns: new[] { "CompanyId", "DomainId", "CategoryId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Url",
                table: "Companies",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserName",
                table: "Companies",
                column: "UserName",
                unique: true);
        }
    }
}
