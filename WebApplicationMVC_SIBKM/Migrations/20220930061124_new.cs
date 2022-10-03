using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationMVC_SIBKM.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kecamatan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kecamatan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kelurahan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    KecamatanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kelurahan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kelurahan_Kecamatan_KecamatanId",
                        column: x => x.KecamatanId,
                        principalTable: "Kecamatan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kelurahan_KecamatanId",
                table: "Kelurahan",
                column: "KecamatanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kelurahan");

            migrationBuilder.DropTable(
                name: "Kecamatan");
        }
    }
}
