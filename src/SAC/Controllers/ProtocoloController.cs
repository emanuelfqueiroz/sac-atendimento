using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SAC.Controllers.Common;
using SAC.Controllers.Requests;
using SAC.Data;
using SAC.Models;

namespace SAC.Controllers
{
    public class ProtocoloController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProtocoloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Protocolo
        public async Task<IActionResult> Index([FromQuery] PesquisaProtocolo request)
        {
            var query = _context.Protocolos
                .Include(p => p.Assunto)
                .Include(p => p.Cliente)
                .Include(p => p.Motivo)
                .Include(p => p.Setor).AsQueryable();

            query = Filtra(query, request);
            ViewBag.Filtro = request;
            return View(await query.ToListAsync());
        }

        private IQueryable<Protocolo> Filtra(IQueryable<Protocolo> query, PesquisaProtocolo request)
        {
            if (request != null)
            {
                if (!String.IsNullOrEmpty(request.ProtocoloId))
                    query = query.Where(p => p.ProtocoloId == request.ProtocoloId);
                if (!String.IsNullOrEmpty(request.DigitroId))
                    query = query.Where(p => p.DigitroId == request.DigitroId);
                if (!String.IsNullOrEmpty(request.Nome))
                    query = query.Where(p => p.Cliente.Nome.Contains(request.Nome));
                if (!String.IsNullOrEmpty(request.Documento))
                    query = query.Where(p => p.ClienteId == request.Documento);
            }
            return query;
        }



        // GET: Protocolo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocolo = await _context.Protocolos
                .Include(p => p.Assunto)
                .Include(p => p.Cliente)
                .Include(p => p.Motivo)
                .Include(p => p.Setor)
                .FirstOrDefaultAsync(m => m.ProtocoloId == id);
            if (protocolo == null)
            {
                return NotFound();
            }

            return View(protocolo);
        }

        
        public async Task<IActionResult> Create(string id)
        {
            var model = new Protocolo(true);
            model.Cliente = _context.Cliente.Single(c => c.Documento == id);
            model.ClienteId = id;
            LoadSelectList(model.Cliente);
            return View(model);
        }

      
        private void LoadSelectList(Cliente cliente)
        {
            ViewData["AssuntoId"] = new SelectList(_context.Assunto, nameof(Assunto.Id), nameof(Assunto.Nome));
            ViewData["ClienteId"] = new SelectList(new List<Cliente>() { cliente}, nameof(Cliente.Documento), nameof(Assunto.Nome));
            ViewData["MotivoId"] = new SelectList(_context.Motivo,
                nameof(Motivo.Id),
                nameof(Motivo.Nome));
            ViewData["SetorId"] = new SelectList(_context.Setor, nameof(Setor.Id), nameof(Setor.Nome));
        }

        // POST: Protocolo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProtocoloId,DigitroId,ClienteId,SetorId,Observacao,MotivoId,AssuntoId,DtCriacao,DtAlterado")] Protocolo protocolo)
        {
            ValidaChavesUnitarias(protocolo);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(protocolo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exc)
                {
                    ViewBag.Error = exc.Message;
                }
            }
            LoadSelectList(protocolo.Cliente);
            return View(protocolo);
        }

        private void ValidaChavesUnitarias(Protocolo protocolo)
        {
            if (ProtocoloExists(protocolo.ProtocoloId))
            {
                ModelState.AddModelError(nameof(protocolo.ProtocoloId), "Protocolo já existe");
            }
            if (DigitroIdExists(protocolo.DigitroId))
            {
                ModelState.AddModelError(nameof(protocolo.DigitroId), "Digitro já existe");
            }
        }

        // GET: Protocolo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protocolo = await _context.Protocolos.Include(c => c.Cliente).SingleAsync(d => d.ProtocoloId ==  id);
            if (protocolo == null)
            {
                return NotFound();
            }

            LoadSelectList(protocolo.Cliente);
            return View(protocolo);
        }

        // POST: Protocolo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProtocoloId,DigitroId,ClienteId,SetorId,Observacao,MotivoId,AssuntoId,DtCriacao,DtAlterado")] Protocolo protocolo)
        {
            if (id != protocolo.ProtocoloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(protocolo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtocoloExists(protocolo.ProtocoloId))
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
            LoadSelectList(await _context.Cliente.FindAsync(protocolo.ClienteId));
            return View(protocolo);
        }
       
        private bool ProtocoloExists(string id)
        {
            return _context.Protocolos.Any(e => e.ProtocoloId == id);
        }
        private bool DigitroIdExists(string digitroId)
        {
            return _context.Protocolos.Any(e => e.DigitroId == digitroId);
        }
    }
}
