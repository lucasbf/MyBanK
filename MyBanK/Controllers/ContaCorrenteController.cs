using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBanK.Data;
using MyBanK.Models;

namespace MyBanK.Controllers
{
    public class ContaCorrenteController : Controller
    {
        private readonly MyBanKContext _context;

        public ContaCorrenteController(MyBanKContext context)
        {
            _context = context;
        }

        // GET: ContaCorrente
        public async Task<IActionResult> Index()
        {
            var contasCorrentes = await _context.ContaCorrente.ToListAsync();
            return View(contasCorrentes);
        }

        // GET: ContaCorrente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContaCorrente == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContaCorrente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            return View(contaCorrente);
        }

        public async Task<IActionResult> Movimento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contaCorrente = await _context.ContaCorrente.Include("Movimentos")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }
            return View("Movimentacao", contaCorrente);
        }

        public async Task<IActionResult> Saque(int? id, string? descricao, double? valor)
        {
            if (id == null || descricao == null || valor == null)
            {
                return NotFound();
            }
            var movimento = new Movimento
            {
                ContaCorrenteId = id.Value,
                Ocorrencia = DateTime.Now,
                Descr = descricao,
                Valor = -valor.Value
            };
            await _context.Movimentos.AddAsync(movimento);
            await _context.SaveChangesAsync();
            var contaCorrente = await _context.ContaCorrente.Include("Movimentos")
                .FirstOrDefaultAsync(m => m.Id == id);
            return View("Movimentacao", contaCorrente);
        }

        public async Task<IActionResult> Deposito(int? id, string? descricao, double? valor)
        {
            if (id == null || descricao == null || valor == null)
            {
                return NotFound();
            }
            var movimento = new Movimento
            {
                ContaCorrenteId = id.Value,
                Ocorrencia = DateTime.Now,
                Descr = descricao,
                Valor = valor.Value
            };
            await _context.Movimentos.AddAsync(movimento);
            await _context.SaveChangesAsync();
            var contaCorrente = await _context.ContaCorrente.Include("Movimentos")
                .FirstOrDefaultAsync(m => m.Id == id);
            return View("Movimentacao", contaCorrente);
        }

        // GET: ContaCorrente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContaCorrente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titular,DataAbertura,Saldo")] ContaCorrente contaCorrente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaCorrente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contaCorrente);
        }

        // GET: ContaCorrente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContaCorrente == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContaCorrente.FindAsync(id);
            if (contaCorrente == null)
            {
                return NotFound();
            }
            return View(contaCorrente);
        }

        // POST: ContaCorrente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titular,DataAbertura,Saldo")] ContaCorrente contaCorrente)
        {
            if (id != contaCorrente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaCorrente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaCorrenteExists(contaCorrente.Id))
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
            return View(contaCorrente);
        }

        // GET: ContaCorrente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContaCorrente == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContaCorrente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            return View(contaCorrente);
        }

        // POST: ContaCorrente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContaCorrente == null)
            {
                return Problem("Entity set 'MyBanKContext.ContaCorrente'  is null.");
            }
            var contaCorrente = await _context.ContaCorrente.FindAsync(id);
            if (contaCorrente != null)
            {
                _context.ContaCorrente.Remove(contaCorrente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaCorrenteExists(int id)
        {
          return (_context.ContaCorrente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
