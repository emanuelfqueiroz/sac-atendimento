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
    public class AssuntoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssuntoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assuntos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assunto.ToListAsync());
        }

        // GET: Assuntos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assunto = await _context.Assunto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assunto == null)
            {
                return NotFound();
            }

            return View(assunto);
        }

        // GET: Assuntos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assuntos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Assunto assunto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assunto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assunto);
        }

        // GET: Assuntos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assunto = await _context.Assunto.FindAsync(id);
            if (assunto == null)
            {
                return NotFound();
            }
            return View(assunto);
        }

        // POST: Assuntos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Assunto assunto)
        {
            if (id != assunto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assunto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssuntoExists(assunto.Id))
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
            return View(assunto);
        }

        // GET: Assuntos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assunto = await _context.Assunto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assunto == null)
            {
                return NotFound();
            }

            return View(assunto);
        }

        // POST: Assuntos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assunto = await _context.Assunto.FindAsync(id);
            _context.Assunto.Remove(assunto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssuntoExists(int id)
        {
            return _context.Assunto.Any(e => e.Id == id);
        }
    }
}
