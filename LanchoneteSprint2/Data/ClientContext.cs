using LanchoneteSprint2.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Lanchonete.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<CadProduto> CaProduto { get; set; }
        public DbSet<VendeItem> VendeItem { get; set; }
        public DbSet<CadVendas> CadVendas { get; set; }
        public DbSet<CadCompras> CadCompras { get; set; }
        public DbSet<CompraItem> CompraItens { get; set; }
        public DbSet<RelatorioProduto> RelatorioProd { get; set; }
        public object CadProduto { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Fornecedor>().ToTable("tbFornecedores");
            modelBuilder.Entity<Produto>().ToTable("tbProdutos");
            modelBuilder.Entity<CadProduto>().ToTable("tbCadProduto");
            modelBuilder.Entity<VendeItem>().ToTable("tbVendeItem");
            modelBuilder.Entity<CadVendas>().ToTable("tbCadVendas");
            modelBuilder.Entity<CadCompras>().ToTable("tbCadCompras");
            modelBuilder.Entity<CompraItem>().ToTable("tbCompraItem");
            modelBuilder.Entity<RelatorioProduto>().ToTable("tbRelatorioProd");
        }
        
    }
}
