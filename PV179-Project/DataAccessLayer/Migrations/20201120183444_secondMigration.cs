using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccessLayer.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownvoteCount",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Flagged",
                table: "Reviews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UpvoteCount",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PermanentlyAdded",
                table: "Locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserReviewVote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssociatedUserId = table.Column<int>(nullable: false),
                    AssociatedReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReviewVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReviewVote_Reviews_AssociatedReviewId",
                        column: x => x.AssociatedReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReviewVote_Users_AssociatedUserId",
                        column: x => x.AssociatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReviewVote_AssociatedReviewId",
                table: "UserReviewVote",
                column: "AssociatedReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReviewVote_AssociatedUserId",
                table: "UserReviewVote",
                column: "AssociatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReviewVote");

            migrationBuilder.DropColumn(
                name: "DownvoteCount",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Flagged",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpvoteCount",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PermanentlyAdded",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Locations");
        }
    }
}
