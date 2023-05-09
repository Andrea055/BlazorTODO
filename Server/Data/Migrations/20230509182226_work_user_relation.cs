using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_blazor_auth.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class work_user_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserWork");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Works",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Works",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Works_UserId1",
                table: "Works",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_UserId1",
                table: "Works",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_UserId1",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_UserId1",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Works");

            migrationBuilder.CreateTable(
                name: "ApplicationUserWork",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    WorkId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserWork", x => new { x.UserId, x.WorkId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserWork_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserWork_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserWork_WorkId",
                table: "ApplicationUserWork",
                column: "WorkId");
        }
    }
}
