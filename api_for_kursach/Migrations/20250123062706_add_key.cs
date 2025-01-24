using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_for_kursach.Migrations
{
    /// <inheritdoc />
    public partial class add_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_RoleId",
                table: "Artists",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Roles_RoleId",
                table: "Artists",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Roles_RoleId",
                table: "Artists");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Artists_RoleId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Artists");
        }
    }
}
