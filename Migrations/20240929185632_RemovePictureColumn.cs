using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi4_PixieVault_DemiBruls.Migrations
{
    /// <inheritdoc />
    public partial class RemovePictureColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Picture_CollectionItemPictureId",
                table: "UserItems");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_CollectionItemPictureId",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "CollectionItemPictureId",
                table: "UserItems");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "UserItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "UserItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionItemPictureId",
                table: "UserItems",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_CollectionItemPictureId",
                table: "UserItems",
                column: "CollectionItemPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Picture_CollectionItemPictureId",
                table: "UserItems",
                column: "CollectionItemPictureId",
                principalTable: "Picture",
                principalColumn: "Id");
        }
    }
}
