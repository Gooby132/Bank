using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    tz = table.Column<string>(type: "text", nullable: false),
                    full_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    english_full_name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    currency = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.tz);
                });

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    AccountUserTz = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => new { x.AccountUserTz, x.Id });
                    table.ForeignKey(
                        name: "FK_operations_users_AccountUserTz",
                        column: x => x.AccountUserTz,
                        principalTable: "users",
                        principalColumn: "tz",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operations");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
