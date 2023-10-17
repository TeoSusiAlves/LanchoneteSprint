using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lanchonete.Data;
using LanchoneteSprint2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LanchoneteSprint2.Controllers
{
    public class ItensCompraController : Controller
    {
        private readonly ClientContext _context;

        public ItensCompraController(ClientContext context)
        {
            _context = context;
        }

        // GET: ItensCompra
        [Authorize(Roles = "Admin, Estagiario")]

        public async Task<IActionResult> Index(string? id)
        {
            var clientContext = _context.CompraItens.Where(i => i.CadComprasId.ToString() == id).Include(v => v.CadCompras).Include(v => v.Produto);
            if (id != null)
            {
                ViewData["CompraId"] = id;
            }
            return View(await clientContext.ToListAsync());
        }


        // GET: ItensCompra/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CompraItens == null)
            {
                return NotFound();
            }

            var compraItem = await _context.CompraItens
                .Include(c => c.CadCompras)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.CompraItemId == id);
            if (compraItem == null)
            {
                return NotFound();
            }

            return View(compraItem);
        }

        // GET: ItensCompra/Create
        public IActionResult Create(string? id)
        {
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "Nota");
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome");
            ViewData["CompraId"] = id;
            return View();
        }

        // POST: ItensCompra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraItemId,ProdutoId,CadComprasId,Qtd,Preco")] CompraItem compraItem)
        {
            if (ModelState.IsValid)
            {
                compraItem.CompraItemId = Guid.NewGuid();
                _context.Add(compraItem);
                await _context.SaveChangesAsync();
                string id = compraItem.CadComprasId.ToString();

                var prod = _context.Produto.FirstOrDefault(i => i.ProdutoId == compraItem.ProdutoId);
                prod.Estoque += compraItem.Qtd;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "ItensCompra", new { id = id });
            }
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "Nota", compraItem.CadComprasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome", compraItem.ProdutoId);
            return View(compraItem);
        }

        // GET: ItensCompra/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CompraItens == null)
            {
                return NotFound();
            }

            var compraItem = await _context.CompraItens.FindAsync(id);
            if (compraItem == null)
            {
                return NotFound();
            }
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "Nota", compraItem.CadComprasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome", compraItem.ProdutoId);
            return View(compraItem);
        }

        // POST: ItensCompra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompraItemId,ProdutoId,CadComprasId,Qtd,Preco")] CompraItem compraItem)
        {
            if (id != compraItem.CompraItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compraItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraItemExists(compraItem.CompraItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "Nota", compraItem.CadComprasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome", compraItem.ProdutoId);
            return View(compraItem);
        }

        // GET: ItensCompra/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CompraItens == null)
            {
                return NotFound();
            }

            var compraItem = await _context.CompraItens
                .Include(c => c.CadCompras)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.CompraItemId == id);
            if (compraItem == null)
            {
                return NotFound();
            }

            return View(compraItem);
        }

        // POST: ItensCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CompraItens == null)
            {
                return Problem("Entity set 'ClientContext.CompraItens'  is null.");
            }
            var compraItem = await _context.CompraItens.FindAsync(id);
            if (compraItem != null)
            {
                _context.CompraItens.Remove(compraItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraItemExists(Guid id)
        {
          return (_context.CompraItens?.Any(e => e.CompraItemId == id)).GetValueOrDefault();
        }
    }
}
