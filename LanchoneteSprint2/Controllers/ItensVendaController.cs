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
    public class ItensVendaController : Controller
    {
        private readonly ClientContext _context;

        public ItensVendaController(ClientContext context)
        {
            _context = context;
        }

        // GET: ItensVenda

        [Authorize(Roles = "Admin, Estagiario")]
        public async Task<IActionResult> Index(string? id)
        {
            var clientContext = _context.VendeItem.Where(i => i.CadVendasId.ToString() == id).Include(v => v.CadVendas).Include(v => v.Produto);
            if(id != null)
            {
                ViewData["VendaId"] = id;
            }
            return View(await clientContext.ToListAsync());
        }


        // GET: ItensVenda/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.VendeItem == null)
            {
                return NotFound();
            }

            var vendeItem = await _context.VendeItem
                .Include(v => v.CadVendas)
                .Include(v => v.Produto)
                .FirstOrDefaultAsync(m => m.VendeItemId == id);
            if (vendeItem == null)
            {
                return NotFound();
            }

            return View(vendeItem);
        }

        // GET: ItensVenda/Create
        public IActionResult Create(string? id)
        {
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "Nota");
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome");
            ViewData["VendaId"] = id;
            return View();
        }

        // POST: ItensVenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendeItemId,ProdutoId,CadVendasId,Qtd,Preco")] VendeItem vendeItem)
        {
            if (ModelState.IsValid)
            {
                vendeItem.VendeItemId = Guid.NewGuid();
                _context.Add(vendeItem);
                await _context.SaveChangesAsync();
                string id = vendeItem.CadVendasId.ToString();

                var prod = _context.Produto.FirstOrDefault(i => i.ProdutoId == vendeItem.ProdutoId);
                prod.Estoque -= vendeItem.Qtd;
                await _context.SaveChangesAsync();



                return RedirectToAction( "Index", "ItensVenda", new { id = id } );
            }
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "Nota", vendeItem.CadVendasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome", vendeItem.ProdutoId);
            return View(vendeItem);
        }

        // GET: ItensVenda/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.VendeItem == null)
            {
                return NotFound();
            }

            var vendeItem = await _context.VendeItem.FindAsync(id);
            if (vendeItem == null)
            {
                return NotFound();
            }
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "Nota", vendeItem.CadVendasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome", vendeItem.ProdutoId);
            return View(vendeItem);
        }

        // POST: ItensVenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VendeItemId,ProdutoId,CadVendasId,Qtd,Preco")] VendeItem vendeItem)
        {
            if (id != vendeItem.VendeItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendeItemExists(vendeItem.VendeItemId))
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
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "Nota", vendeItem.CadVendasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "ProdutoId", "Nome", vendeItem.ProdutoId);
            return View(vendeItem);
        }

        // GET: ItensVenda/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.VendeItem == null)
            {
                return NotFound();
            }

            var vendeItem = await _context.VendeItem
                .Include(v => v.CadVendas)
                .Include(v => v.Produto)
                .FirstOrDefaultAsync(m => m.VendeItemId == id);
            if (vendeItem == null)
            {
                return NotFound();
            }

            return View(vendeItem);
        }

        // POST: ItensVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.VendeItem == null)
            {
                return Problem("Entity set 'ClientContext.VendeItem'  is null.");
            }
            var vendeItem = await _context.VendeItem.FindAsync(id);
            if (vendeItem != null)
            {
                _context.VendeItem.Remove(vendeItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendeItemExists(Guid id)
        {
          return (_context.VendeItem?.Any(e => e.VendeItemId == id)).GetValueOrDefault();
        }
    }
}
