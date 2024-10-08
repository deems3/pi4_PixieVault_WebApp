using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi4_PixieVault_DemiBruls.Migrations
{
    /// <inheritdoc />
    public partial class picturetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_AspNetUsers_UserId",
                table: "UserItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "UserItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "CollectionItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_PictureId",
                table: "UserItems",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_PictureId",
                table: "CollectionItems",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_Pictures_PictureId",
                table: "CollectionItems",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_AspNetUsers_UserId",
                table: "UserItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Pictures_PictureId",
                table: "UserItems",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_Pictures_PictureId",
                table: "CollectionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_AspNetUsers_UserId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Pictures_PictureId",
                table: "UserItems");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_PictureId",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_CollectionItems_PictureId",
                table: "CollectionItems");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "CollectionItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_AspNetUsers_UserId",
                table: "UserItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
