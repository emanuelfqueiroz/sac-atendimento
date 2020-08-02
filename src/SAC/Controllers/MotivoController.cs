using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAC.Data;
using SAC.Models;

namespace SAC.Controllers
{
    public class MotivoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotivoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Motivo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Motivo.ToListAsync());
        }

        // GET: Motivo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivo = await _context.Motivo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motivo == null)
            {
                return NotFound();
            }

            return View(motivo);
        }

        // GET: Motivo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motivo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Motivo motivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motivo);
        }

        // GET: Motivo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivo = await _context.Motivo.FindAsync(id);
            if (motivo == null)
            {
                return NotFound();
            }
            return View(motivo);
        }

        // POST: Motivo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Motivo motivo)
        {
            if (id != motivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotivoExists(motivo.Id))
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
            return View(motivo);
        }

        // GET: Motivo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivo = await _context.Motivo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motivo == null)
            {
                return NotFound();
            }

            return View(motivo);
        }

        // POST: Motivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motivo = await _context.Motivo.FindAsync(id);
            _context.Motivo.Remove(motivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotivoExists(int id)
        {
            return _context.Motivo.Any(e => e.Id == id);
        }
    }
}
