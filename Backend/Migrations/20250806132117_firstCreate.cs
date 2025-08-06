using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class firstCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OfficialIconName = table.Column<string>(type: "longtext", nullable: false, comment: "アイコン名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefalultIconName = table.Column<string>(type: "longtext", nullable: false, comment: "表示名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NewsletterTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MailTitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "メールタイトル")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MailBody = table.Column<string>(type: "varchar(5120)", maxLength: 5120, nullable: false, comment: "メール本文")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterTemplates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserHash = table.Column<string>(type: "longtext", nullable: false, comment: "ユーザーHash")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false, comment: "ユーザー名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false, comment: "メールアドレス")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kakeibo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "ユーザーID"),
                    KakeiboName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "家計簿名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KakeiboExplanation = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "説明文")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kakeibo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kakeibo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KakeiboItemOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KakeiboId = table.Column<int>(type: "int", nullable: false, comment: "家計簿ID"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "カテゴリID"),
                    ItemName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "名前")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemAmount = table.Column<int>(type: "int", nullable: false, comment: "金額"),
                    InoutFlg = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "出入金フラグ"),
                    Frequency = table.Column<int>(type: "int", nullable: false, comment: "固定費頻度"),
                    FixedStartDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 20, nullable: false, comment: "固定費開始日時"),
                    FixedEndDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 20, nullable: false, comment: "固定費終了日時"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KakeiboItemOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KakeiboItemOptions_Kakeibo_KakeiboId",
                        column: x => x.KakeiboId,
                        principalTable: "Kakeibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KakeiboItemOptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KakeiboID = table.Column<int>(type: "int", nullable: false, comment: "家計簿テーブルID"),
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
                    table.PrimaryKey("PK_Kategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategories_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kategories_Kakeibo_KakeiboID",
                        column: x => x.KakeiboID,
                        principalTable: "Kakeibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KakeiboItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KakeiboId = table.Column<int>(type: "int", nullable: false, comment: "家計簿テーブルID"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "カテゴリID"),
                    ItemName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "名前")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemAmount = table.Column<int>(type: "int", nullable: false, comment: "金額"),
                    InoutFlg = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "出入金フラグ"),
                    UsedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "出入金日付"),
                    ItemOptionId = table.Column<int>(type: "int", nullable: false, comment: "オプションID"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登録日時"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時"),
                    DeleteDate = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KakeiboItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KakeiboItems_KakeiboItemOptions_ItemOptionId",
                        column: x => x.ItemOptionId,
                        principalTable: "KakeiboItemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KakeiboItems_Kakeibo_KakeiboId",
                        column: x => x.KakeiboId,
                        principalTable: "Kakeibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KakeiboItems_Kategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Kategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Kakeibo_UserId",
                table: "Kakeibo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItemOptions_KakeiboId",
                table: "KakeiboItemOptions",
                column: "KakeiboId");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItemOptions_UserId",
                table: "KakeiboItemOptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItems_CategoryId",
                table: "KakeiboItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItems_ItemOptionId",
                table: "KakeiboItems",
                column: "ItemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_KakeiboItems_KakeiboId",
                table: "KakeiboItems",
                column: "KakeiboId");

            migrationBuilder.CreateIndex(
                name: "IX_Kategories_IconId",
                table: "Kategories",
                column: "IconId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategories_KakeiboID",
                table: "Kategories",
                column: "KakeiboID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KakeiboItems");

            migrationBuilder.DropTable(
                name: "NewsletterTemplates");

            migrationBuilder.DropTable(
                name: "KakeiboItemOptions");

            migrationBuilder.DropTable(
                name: "Kategories");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "Kakeibo");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
