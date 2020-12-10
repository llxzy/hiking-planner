using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReviewVote_Reviews_AssociatedReviewId",
                table: "UserReviewVote");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviewVote_Users_AssociatedUserId",
                table: "UserReviewVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReviewVote",
                table: "UserReviewVote");

            migrationBuilder.RenameTable(
                name: "UserReviewVote",
                newName: "UserReviewVotes");

            migrationBuilder.RenameIndex(
                name: "IX_UserReviewVote_AssociatedReviewId",
                table: "UserReviewVotes",
                newName: "IX_UserReviewVotes_AssociatedReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReviewVotes",
                table: "UserReviewVotes",
                columns: new[] { "AssociatedUserId", "AssociatedReviewId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviewVotes_Reviews_AssociatedReviewId",
                table: "UserReviewVotes",
                column: "AssociatedReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviewVotes_Users_AssociatedUserId",
                table: "UserReviewVotes",
                column: "AssociatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReviewVotes_Reviews_AssociatedReviewId",
                table: "UserReviewVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviewVotes_Users_AssociatedUserId",
                table: "UserReviewVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReviewVotes",
                table: "UserReviewVotes");

            migrationBuilder.RenameTable(
                name: "UserReviewVotes",
                newName: "UserReviewVote");

            migrationBuilder.RenameIndex(
                name: "IX_UserReviewVotes_AssociatedReviewId",
                table: "UserReviewVote",
                newName: "IX_UserReviewVote_AssociatedReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReviewVote",
                table: "UserReviewVote",
                columns: new[] { "AssociatedUserId", "AssociatedReviewId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviewVote_Reviews_AssociatedReviewId",
                table: "UserReviewVote",
                column: "AssociatedReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviewVote_Users_AssociatedUserId",
                table: "UserReviewVote",
                column: "AssociatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
