using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Repository.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 20, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 20, nullable: false),
                    Preço = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Descrição = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Imagem = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
