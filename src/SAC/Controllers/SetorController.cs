﻿using System;
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
    public class SetorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SetorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Setor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Setor.ToListAsync());
        }

        // GET: Setor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _context.Setor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setor == null)
            {
                return NotFound();
            }

            return View(setor);
        }

        // GET: Setor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Setor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Setor setor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setor);
        }

        // GET: Setor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _context.Setor.FindAsync(id);
            if (setor == null)
            {
                return NotFound();
            }
            return View(setor);
        }

        // POST: Setor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Setor setor)
        {
            if (id != setor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetorExists(setor.Id))
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
            return View(setor);
        }

        // GET: Setor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _context.Setor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setor == null)
            {
                return NotFound();
            }

            return View(setor);
        }

        // POST: Setor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setor = await _context.Setor.FindAsync(id);
            _context.Setor.Remove(setor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetorExists(int id)
        {
            return _context.Setor.Any(e => e.Id == id);
        }
    }
}
