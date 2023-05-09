using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_blazor_auth.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_user_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Works",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Works");
        }
    }
}
