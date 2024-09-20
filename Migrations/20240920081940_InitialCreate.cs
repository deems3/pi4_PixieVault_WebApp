using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi4_PixieVault_DemiBruls.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItemPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemPictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CollectionItemPictureId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItems_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItems_CollectionItemPictures_CollectionItemPictureId",
                        column: x => x.CollectionItemPictureId,
                        principalTable: "CollectionItemPictures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionItemsCopies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsForSale = table.Column<bool>(type: "bit", nullable: false),
                    CollectionItemId = table.Column<int>(type: "int", nullable: false),
                    CollectionItemPictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemsCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                        column: x => x.CollectionItemPictureId,
                        principalTable: "CollectionItemPictures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionItemsCopies_CollectionItems_CollectionItemId",
                        column: x => x.CollectionItemId,
                        principalTable: "CollectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItemsCopies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItemCopyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectionItemCopyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemCopyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItemCopyValues_CollectionItemsCopies_CollectionItemCopyId",
                        column: x => x.CollectionItemCopyId,
                        principalTable: "CollectionItemsCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemCopyValues_CollectionItemCopyId",
                table: "CollectionItemCopyValues",
                column: "CollectionItemCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_BrandId",
                table: "CollectionItems",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_CategoryId",
                table: "CollectionItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_CollectionItemPictureId",
                table: "CollectionItems",
                column: "CollectionItemPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemsCopies_CollectionItemId",
                table: "CollectionItemsCopies",
                column: "CollectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemsCopies_CollectionItemPictureId",
                table: "CollectionItemsCopies",
                column: "CollectionItemPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemsCopies_UserId",
                table: "CollectionItemsCopies",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItemCopyValues");

            migrationBuilder.DropTable(
                name: "CollectionItemsCopies");

            migrationBuilder.DropTable(
                name: "CollectionItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CollectionItemPictures");
        }
    }
}
