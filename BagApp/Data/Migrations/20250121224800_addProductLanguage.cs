using Microsoft.EntityFrameworkCore.Migrations;

namespace BagApp.Data.Migrations
{
    public partial class addProductLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "Products");
        }
    }
}
