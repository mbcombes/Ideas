using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpExam.Migrations
{
    public partial class likestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Ideas_IdeaId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Users_UserId",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_IdeaId",
                table: "Likes",
                newName: "IX_Likes_IdeaId");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Ideas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "LikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Ideas_IdeaId",
                table: "Likes",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "IdeaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Ideas_IdeaId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_IdeaId",
                table: "Like",
                newName: "IX_Like_IdeaId");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Ideas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "LikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Ideas_IdeaId",
                table: "Like",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "IdeaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Users_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
