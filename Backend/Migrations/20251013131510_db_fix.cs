using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class db_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefalultIconName",
                table: "Icon",
                newName: "DefaultIconName");

            migrationBuilder.RenameColumn(
                name: "KategoryName",
                table: "Category",
                newName: "CategoryName");

            migrationBuilder.CreateTable(
                name: "CategoryDefault",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KategoryName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "カテゴリ名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InoutFlg = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "出入金フラグ"),
                    IconId = table.Column<int>(type: "int", nullable: false, comment: "アイコンID"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDefault", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryDefault_Icon_IconId",
                        column: x => x.IconId,
                        principalTable: "Icon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDefault_IconId",
                table: "CategoryDefault",
                column: "IconId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDefault");

            migrationBuilder.RenameColumn(
                name: "DefaultIconName",
                table: "Icon",
                newName: "DefalultIconName");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Category",
                newName: "KategoryName");
        }
    }
}
