using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccessLayer.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReviewVote",
                table: "UserReviewVote");

            migrationBuilder.DropIndex(
                name: "IX_UserReviewVote_AssociatedUserId",
                table: "UserReviewVote");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserReviewVote",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReviewVote",
                table: "UserReviewVote",
                columns: new[] { "AssociatedUserId", "AssociatedReviewId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReviewVote",
                table: "UserReviewVote");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserReviewVote",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReviewVote",
                table: "UserReviewVote",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserReviewVote_AssociatedUserId",
                table: "UserReviewVote",
                column: "AssociatedUserId");
        }
    }
}
