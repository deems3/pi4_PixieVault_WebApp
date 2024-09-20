using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi4_PixieVault_DemiBruls.Migrations
{
    /// <inheritdoc />
    public partial class removepicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItemsCopies");

            migrationBuilder.DropIndex(
                name: "IX_CollectionItems_CollectionItemPictureId",
                table: "CollectionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionItemPictures",
                table: "CollectionItemPictures");

            migrationBuilder.DropColumn(
                name: "CollectionItemPictureId",
                table: "CollectionItems");

            migrationBuilder.RenameTable(
                name: "CollectionItemPictures",
                newName: "CollectionItemPicture");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionItemPicture",
                table: "CollectionItemPicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPicture_CollectionItemPictureId",
                table: "CollectionItemsCopies",
                column: "CollectionItemPictureId",
                principalTable: "CollectionItemPicture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPicture_CollectionItemPictureId",
                table: "CollectionItemsCopies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionItemPicture",
                table: "CollectionItemPicture");

            migrationBuilder.RenameTable(
                name: "CollectionItemPicture",
                newName: "CollectionItemPictures");

            migrationBuilder.AddColumn<int>(
                name: "CollectionItemPictureId",
                table: "CollectionItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionItemPictures",
                table: "CollectionItemPictures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_CollectionItemPictureId",
                table: "CollectionItems",
                column: "CollectionItemPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItems",
                column: "CollectionItemPictureId",
                principalTable: "CollectionItemPictures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItemsCopies",
                column: "CollectionItemPictureId",
                principalTable: "CollectionItemPictures",
                principalColumn: "Id");
        }
    }
}
