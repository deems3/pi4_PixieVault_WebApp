using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi4_PixieVault_DemiBruls.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaxLengthFromPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItemsCopies");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionItemPictureId",
                table: "CollectionItemsCopies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItemsCopies",
                column: "CollectionItemPictureId",
                principalTable: "CollectionItemPictures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItemsCopies");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionItemPictureId",
                table: "CollectionItemsCopies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItemsCopies_CollectionItemPictures_CollectionItemPictureId",
                table: "CollectionItemsCopies",
                column: "CollectionItemPictureId",
                principalTable: "CollectionItemPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
