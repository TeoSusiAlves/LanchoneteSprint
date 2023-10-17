using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchoneteSprint2.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbCadProduto",
                columns: table => new
                {
                    CadProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCadProduto", x => x.CadProdutoId);
                });

            migrationBuilder.CreateTable(
                name: "tbClientes",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereço = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "tbFornecedores",
                columns: table => new
                {
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFornecedores", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "tbCadVendas",
                columns: table => new
                {
                    CadVendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCadVendas", x => x.CadVendasId);
                    table.ForeignKey(
                        name: "FK_tbCadVendas_tbClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "tbClientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbCadCompras",
                columns: table => new
                {
                    CadComprasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCadCompras", x => x.CadComprasId);
                    table.ForeignKey(
                        name: "FK_tbCadCompras_tbFornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "tbFornecedores",
                        principalColumn: "FornecedorId");
                });

            migrationBuilder.CreateTable(
                name: "tbProdutos",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false),
                    PrecoUn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CadProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadComprasId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CadVendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProdutos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbCadCompras_CadComprasId",
                        column: x => x.CadComprasId,
                        principalTable: "tbCadCompras",
                        principalColumn: "CadComprasId");
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbCadProduto_CadProdutoId",
                        column: x => x.CadProdutoId,
                        principalTable: "tbCadProduto",
                        principalColumn: "CadProdutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbCadVendas_CadVendasId",
                        column: x => x.CadVendasId,
                        principalTable: "tbCadVendas",
                        principalColumn: "CadVendasId");
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbFornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "tbFornecedores",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbCompraItem",
                columns: table => new
                {
                    CompraItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadComprasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qtd = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCompraItem", x => x.CompraItemId);
                    table.ForeignKey(
                        name: "FK_tbCompraItem_tbCadCompras_CadComprasId",
                        column: x => x.CadComprasId,
                        principalTable: "tbCadCompras",
                        principalColumn: "CadComprasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCompraItem_tbProdutos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProdutos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbVendeItem",
                columns: table => new
                {
                    VendeItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadVendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qtd = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbVendeItem", x => x.VendeItemId);
                    table.ForeignKey(
                        name: "FK_tbVendeItem_tbCadVendas_CadVendasId",
                        column: x => x.CadVendasId,
                        principalTable: "tbCadVendas",
                        principalColumn: "CadVendasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbVendeItem_tbProdutos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProdutos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbCadCompras_FornecedorId",
                table: "tbCadCompras",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCadVendas_ClienteId",
                table: "tbCadVendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCompraItem_CadComprasId",
                table: "tbCompraItem",
                column: "CadComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCompraItem_ProdutoId",
                table: "tbCompraItem",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_CadComprasId",
                table: "tbProdutos",
                column: "CadComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_CadProdutoId",
                table: "tbProdutos",
                column: "CadProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_CadVendasId",
                table: "tbProdutos",
                column: "CadVendasId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_FornecedorId",
                table: "tbProdutos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbVendeItem_CadVendasId",
                table: "tbVendeItem",
                column: "CadVendasId");

            migrationBuilder.CreateIndex(
                name: "IX_tbVendeItem_ProdutoId",
                table: "tbVendeItem",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbCompraItem");

            migrationBuilder.DropTable(
                name: "tbVendeItem");

            migrationBuilder.DropTable(
                name: "tbProdutos");

            migrationBuilder.DropTable(
                name: "tbCadCompras");

            migrationBuilder.DropTable(
                name: "tbCadProduto");

            migrationBuilder.DropTable(
                name: "tbCadVendas");

            migrationBuilder.DropTable(
                name: "tbFornecedores");

            migrationBuilder.DropTable(
                name: "tbClientes");
        }
    }
}
