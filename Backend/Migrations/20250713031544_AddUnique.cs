using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserDatas",
                type: "varchar(255)",
                nullable: false,
                comment: "メールアドレス",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "メールアドレス")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_Email",
                table: "UserDatas",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDatas_Email",
                table: "UserDatas");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserDatas",
                type: "longtext",
                nullable: false,
                comment: "メールアドレス",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldComment: "メールアドレス")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
