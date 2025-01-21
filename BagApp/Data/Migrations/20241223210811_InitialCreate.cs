using Microsoft.EntityFrameworkCore.Migrations;

namespace BagApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/uploads/banner.jpg"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Başlık"),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/Uploads/no-image.png"),
                    Stat = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    English = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/uploads/banner.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThemeSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Firma Adı"),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/uploads/logo.png"),
                    Gsm = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "+90 (530) 123 0000"),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "+90 (530) 123 0000"),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Firma hakkında kısa bilgilendirme metni"),
                    ShortAbout = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Firma Footer Bilgisi"),
                    AboutEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortAboutEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortAboutAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Adres Bilgisi"),
                    GoogleVerify = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Favicon = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/uploads/favicon.png"),
                    FooterLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/uploads/logo.png"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Smpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "#"),
                    Youtube = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "#"),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "#"),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    English = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat = table.Column<bool>(type: "bit", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    SeoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/Uploads/no-image.png")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategory_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    English = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubcategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "~/Uploads/no-image.png"),
                    Home = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Stat = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SeoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Subcategory_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "Id", "Image", "LinkUrl" },
                values: new object[] { 1, "~/uploads/banner.jpg", null });

            migrationBuilder.InsertData(
                table: "ThemeSetting",
                columns: new[] { "Id", "About", "AboutAr", "AboutEn", "Address", "CompanyName", "Email", "Email2", "Facebook", "Favicon", "FooterLogoUrl", "GoogleVerify", "Gsm", "LogoUrl", "MetaDesc", "MetaKeyword", "Password", "Phone", "ShortAbout", "ShortAboutAr", "ShortAboutEn", "SiteUrl", "Smpt", "Twitter", "Youtube" },
                values: new object[] { 1, "Firma hakkında kısa bilgilendirme metni", null, null, "Adres Bilgisi", "Firma Adı", "#", "#", "#", "~/uploads/favicon.png", "~/uploads/logo.png", "#", "+90 (530) 123 0000", "~/uploads/logo.png", null, null, null, "+90 (530) 123 0000", "Firma Footer Bilgisi", null, null, "Firma Ünvanı", null, "#", "#" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RoleName", "UserName" },
                values: new object[] { 1, "123", "Admin", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_ProductId",
                table: "ProductMedia",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_CategoryID",
                table: "Subcategory",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "ProductMedia");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "ThemeSetting");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Subcategory");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
