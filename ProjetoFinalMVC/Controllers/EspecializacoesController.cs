using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalMVC.Models;

namespace ProjetoFinalMVC.Controllers
{
    public class EspecializacoesController : Controller
    {
        private readonly Contexto _context;

        public EspecializacoesController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especializacao.OrderBy(e => e.Nome).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especializacao = await _context.Especializacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especializacao == null)
            {
                return NotFound();
            }

            return View(especializacao);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Especializacao especializacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especializacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especializacao);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especializacao = await _context.Especializacao.FindAsync(id);
            if (especializacao == null)
            {
                return NotFound();
            }
            return View(especializacao);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Especializacao especializacao)
        {
            if (id != especializacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especializacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecializacaoExists(especializacao.Id))
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
            return View(especializacao);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especializacao = await _context.Especializacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especializacao == null)
            {
                return NotFound();
            }

            return View(especializacao);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especializacao = await _context.Especializacao.FindAsync(id);
            _context.Especializacao.Remove(especializacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecializacaoExists(int id)
        {
            return _context.Especializacao.Any(e => e.Id == id);
        }
    }
}