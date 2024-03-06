using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Product1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auth_Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    AdminId = table.Column<string>(type: "text", nullable: true),
                    Available = table.Column<bool>(type: "boolean", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ProductPrice = table.Column<int>(type: "integer", nullable: false),
                    IsOffer = table.Column<bool>(type: "boolean", nullable: false),
                    OfferPrice = table.Column<int>(type: "integer", nullable: false),
                    ProductDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_Product_Auth_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Auth_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Product_CategoryId",
                table: "Auth_Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_Product");
        }
    }
}
