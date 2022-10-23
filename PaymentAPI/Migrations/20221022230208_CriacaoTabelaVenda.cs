using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentAPI.Migrations
{
    public partial class CriacaoTabelaVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    IdVenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItensVenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdVendedor = table.Column<int>(type: "int", nullable: false),
                    CPFVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoneVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.IdVenda);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");
        }
    }
}
