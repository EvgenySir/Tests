using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nrbcTestTask.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
					name: "Users",
					columns: table => new {
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
						NormalizedUserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
						PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
						SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_Users", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "UserClaims",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_UserClaims", x => x.Id);
						table.ForeignKey(
											name: "FK_UserClaims_Users_UserId",
											column: x => x.UserId,
											principalTable: "Users",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_UserClaims_UserId",
					table: "UserClaims",
					column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
					name: "UserClaims");

			migrationBuilder.DropTable(
					name: "Users");
		}
	}
}
