using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _8._Music_portal.Models;

namespace _8._Music_portal.Controllers
{
    public class PerformerController : Controller
    {
        private readonly MusicPortalContext _context;

        public PerformerController(MusicPortalContext context)
        {
            _context = context;
        }

        // GET: Performer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Performers.ToListAsync());
        }

        // GET: Performer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerModel = await _context.Performers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performerModel == null)
            {
                return NotFound();
            }

            return View(performerModel);
        }

        // GET: Performer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PerformerModel performerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performerModel);
        }

        // GET: Performer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerModel = await _context.Performers.FindAsync(id);
            if (performerModel == null)
            {
                return NotFound();
            }
            return View(performerModel);
        }

        // POST: Performer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PerformerModel performerModel)
        {
            if (id != performerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformerModelExists(performerModel.Id))
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
            return View(performerModel);
        }

        // GET: Performer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerModel = await _context.Performers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performerModel == null)
            {
                return NotFound();
            }

            return View(performerModel);
        }

        // POST: Performer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performerModel = await _context.Performers.FindAsync(id);
            if (performerModel != null)
            {
                _context.Performers.Remove(performerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerModelExists(int id)
        {
            return _context.Performers.Any(e => e.Id == id);
        }
    }
}
