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
    public class CadProdutosController : Controller
    {
        private readonly ClientContext _context;

        public CadProdutosController(ClientContext context)
        {
            _context = context;
        }

        // GET: CadProdutos
        [Authorize(Roles = "Admin, Estagiario")]
        public async Task<IActionResult> Index()
        {
              return _context.CaProduto != null ? 
                          View(await _context.CaProduto.ToListAsync()) :
                          Problem("Entity set 'ClientContext.CaProduto'  is null.");
        }



        // GET: CadProdutos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CaProduto == null)
            {
                return NotFound();
            }

            var cadProduto = await _context.CaProduto
                .FirstOrDefaultAsync(m => m.CadProdutoId == id);
            if (cadProduto == null)
            {
                return NotFound();
            }

            return View(cadProduto);
        }

        // GET: CadProdutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CadProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CadProdutoId,Nome")] CadProduto cadProduto)
        {
            if (ModelState.IsValid)
            {
                cadProduto.CadProdutoId = Guid.NewGuid();
                _context.Add(cadProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cadProduto);
        }

        // GET: CadProdutos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CaProduto == null)
            {
                return NotFound();
            }

            var cadProduto = await _context.CaProduto.FindAsync(id);
            if (cadProduto == null)
            {
                return NotFound();
            }
            return View(cadProduto);
        }

        // POST: CadProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CadProdutoId,Nome")] CadProduto cadProduto)
        {
            if (id != cadProduto.CadProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadProdutoExists(cadProduto.CadProdutoId))
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
            return View(cadProduto);
        }

        // GET: CadProdutos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CaProduto == null)
            {
                return NotFound();
            }

            var cadProduto = await _context.CaProduto
                .FirstOrDefaultAsync(m => m.CadProdutoId == id);
            if (cadProduto == null)
            {
                return NotFound();
            }

            return View(cadProduto);
        }

        // POST: CadProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CaProduto == null)
            {
                return Problem("Entity set 'ClientContext.CaProduto'  is null.");
            }
            var cadProduto = await _context.CaProduto.FindAsync(id);
            if (cadProduto != null)
            {
                _context.CaProduto.Remove(cadProduto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadProdutoExists(Guid id)
        {
          return (_context.CaProduto?.Any(e => e.CadProdutoId == id)).GetValueOrDefault();
        }
    }
}
