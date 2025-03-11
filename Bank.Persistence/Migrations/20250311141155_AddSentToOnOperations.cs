using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSentToOnOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SentTo",
                table: "operations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentTo",
                table: "operations");
        }
    }
}
