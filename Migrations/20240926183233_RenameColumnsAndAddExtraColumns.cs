using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi4_PixieVault_DemiBruls.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsAndAddExtraColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_Brands_BrandId",
                table: "CollectionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_Categories_CategoryId",
                table: "CollectionItems");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "CollectionItemCopyValues");

            migrationBuilder.DropTable(
                name: "CollectionItemsCopies");

            migrationBuilder.DropTable(
                name: "CollectionItemPicture");

            migrationBuilder.DropIndex(
                name: "IX_CollectionItems_BrandId",
                table: "CollectionItems");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "CollectionItems");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CollectionItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CollectionItemId = table.Column<int>(type: "int", nullable: false),
                    CollectionItemPictureId = table.Column<int>(type: "int", nullable: true),
                    IsForSale = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForSalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserItems_CollectionItems_CollectionItemId",
                        column: x => x.CollectionItemId,
                        principalTable: "CollectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserItems_Picture_CollectionItemPictureId",
                        column: x => x.CollectionItemPictureId,
                        principalTable: "Picture",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_CollectionItemId",
                table: "UserItems",
                column: "CollectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_CollectionItemPictureId",
                table: "UserItems",
                column: "CollectionItemPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_UserId",
                table: "UserItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_Categories_CategoryId",
                table: "CollectionItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_Categories_CategoryId",
                table: "CollectionItems");

            migrationBuilder.DropTable(
                name: "UserItems");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CollectionItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "CollectionItems",
                type: "int",
                nullable: true);

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
                name: "CollectionItemPicture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemPicture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItemsCopies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionItemId = table.Column<int>(type: "int", nullable: false),
                    CollectionItemPictureId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsForSale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemsCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItemsCopies_CollectionItemPicture_CollectionItemPictureId",
                        column: x => x.CollectionItemPictureId,
                        principalTable: "CollectionItemPicture",
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
                    CollectionItemCopyId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                name: "IX_CollectionItems_BrandId",
                table: "CollectionItems",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemCopyValues_CollectionItemCopyId",
                table: "CollectionItemCopyValues",
                column: "CollectionItemCopyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_Brands_BrandId",
                table: "CollectionItems",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_Categories_CategoryId",
                table: "CollectionItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
