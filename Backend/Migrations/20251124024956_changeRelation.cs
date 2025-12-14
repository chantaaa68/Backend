using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class changeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_CategoryDefault_IconId",
            //    table: "CategoryDefault");

            //migrationBuilder.DropIndex(
            //    name: "IX_Category_IconId",
            //    table: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDefault_IconId",
                table: "CategoryDefault",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IconId",
                table: "Category",
                column: "IconId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryDefault_IconId",
                table: "CategoryDefault");

            migrationBuilder.DropIndex(
                name: "IX_Category_IconId",
                table: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDefault_IconId",
                table: "CategoryDefault",
                column: "IconId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_IconId",
                table: "Category",
                column: "IconId",
                unique: true);
        }
    }
}
