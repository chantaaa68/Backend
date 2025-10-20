using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFrequency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedEndDate",
                table: "KakeiboItem");

            migrationBuilder.DropColumn(
                name: "FixedStartDate",
                table: "KakeiboItem");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "KakeiboItem");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyId",
                table: "KakeiboItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "固定費管理ID");

            migrationBuilder.CreateTable(
                name: "KakeiboItemFrequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KakeiboId = table.Column<int>(type: "int", nullable: false, comment: "家計簿テーブルID"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "カテゴリID"),
                    ItemName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "名前")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemAmount = table.Column<int>(type: "int", nullable: false, comment: "金額"),
                    InoutFlg = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "出入金フラグ"),
                    Frequency = table.Column<int>(type: "int", nullable: false, comment: "固定費頻度"),
                    FixedStartDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 20, nullable: true, comment: "固定費開始日時"),
                    FixedEndDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 20, nullable: true, comment: "固定費終了日時"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KakeiboItemFrequency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KakeiboItemFrequency_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KakeiboItemFrequency_Kakeibo_KakeiboId",
                        column: x => x.KakeiboId,
                        principalTable: "Kakeibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItem_FrequencyId",
                table: "KakeiboItem",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItemFrequency_CategoryId",
                table: "KakeiboItemFrequency",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItemFrequency_KakeiboId",
                table: "KakeiboItemFrequency",
                column: "KakeiboId");

            migrationBuilder.AddForeignKey(
                name: "FK_KakeiboItem_KakeiboItemFrequency_FrequencyId",
                table: "KakeiboItem",
                column: "FrequencyId",
                principalTable: "KakeiboItemFrequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KakeiboItem_KakeiboItemFrequency_FrequencyId",
                table: "KakeiboItem");

            migrationBuilder.DropTable(
                name: "KakeiboItemFrequency");

            migrationBuilder.DropIndex(
                name: "IX_KakeiboItem_FrequencyId",
                table: "KakeiboItem");

            migrationBuilder.DropColumn(
                name: "FrequencyId",
                table: "KakeiboItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "FixedEndDate",
                table: "KakeiboItem",
                type: "datetime(6)",
                maxLength: 20,
                nullable: true,
                comment: "固定費終了日時");

            migrationBuilder.AddColumn<DateTime>(
                name: "FixedStartDate",
                table: "KakeiboItem",
                type: "datetime(6)",
                maxLength: 20,
                nullable: true,
                comment: "固定費開始日時");

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "KakeiboItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "固定費頻度");
        }
    }
}
