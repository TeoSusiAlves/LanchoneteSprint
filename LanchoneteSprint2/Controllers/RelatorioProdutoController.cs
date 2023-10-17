using Lanchonete.Data;
using LanchoneteSprint2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LanchoneteSprint2.Controllers
{
    public class RelatorioProdutoController : Controller
    {
        private readonly ClientContext _context;

        public RelatorioProdutoController(ClientContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Gerente")]
        public IActionResult Index()
        {
            var tbProdutos = _context.Produto.Include(c => c.CadProduto);
            var tbCategorias = _context.CadProduto;

            List<RelatorioProduto> ListaProdutos = new List<RelatorioProduto>();

            foreach (var produto in tbProdutos)
            {
                var modelo = new RelatorioProduto();

                modelo.Id = Guid.NewGuid();
                modelo.Quanti = produto.Estoque;
                modelo.Nome = produto.Nome;
                modelo.PrecoUn = produto.PrecoUn;
                modelo.CadProdutoId = produto.CadProdutoId;
                modelo.CadProduto = produto.CadProduto;


                ListaProdutos.Add(modelo);

            }

            return View(ListaProdutos);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? inFiltroProd, string? inFiltroCat)
        {

            var tbProdutos = _context.Produto.Include(c => c.CadProduto);
            var tbCategorias = _context.CadProduto;

            List<RelatorioProduto> ListaFiltro = new List<RelatorioProduto>();

            foreach (var produto in tbProdutos)
            {
                var modelo = new RelatorioProduto();

                modelo.Id = Guid.NewGuid();
                modelo.Quanti = produto.Estoque;
                modelo.Nome = produto.Nome;
                modelo.PrecoUn = produto.PrecoUn;
                modelo.CadProdutoId = produto.CadProdutoId;
                modelo.CadProduto = produto.CadProduto;

                if (inFiltroProd != null)
                {
                    if (modelo.Nome.ToLower().Contains(inFiltroProd.ToLower()))
                    {
                        ListaFiltro.Add(modelo);
                    }
                }

                if (inFiltroCat != null)
                {
                    if (modelo.CadProduto.Nome.ToLower().Contains(inFiltroCat.ToLower()))
                    {
                        ListaFiltro.Add(modelo);
                    }
                }

                if ((inFiltroCat == null) && (inFiltroProd == null))
                {
                    ListaFiltro.Add(modelo);
                }
            }

            return View(ListaFiltro);

        }
    }
}
