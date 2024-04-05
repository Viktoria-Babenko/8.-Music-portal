using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _8._Music_portal.Models;
using _8._Music_portal.NewFolder;

namespace _8._Music_portal.Controllers
{
    public class SongsController : Controller
    {
        IRepository repo;

        public SongsController(IRepository r)
        {
            repo = r;
        }

        // GET: SongsModels
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAllSong());
        }

        // GET: SongsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songsModel = await repo.GetSong(id.Value);
            if (songsModel == null)
            {
                return NotFound();
            }

            return View(songsModel);
        }

        // GET: SongsModels/Create
        public IActionResult Create()
        {
            //ViewData["GenreID"] = new SelectList(_context.Genres, "Id", "Id");
            //ViewData["PerformerID"] = new SelectList(_context.Performers, "Id", "Id");
            return View();
        }

        // POST: SongsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GenreID,PerformerID,Track")] SongsModel songsModel)
        {
            if (ModelState.IsValid)
            {
                await repo.CreateSong(songsModel);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["GenreID"] = new SelectList(_context.Genres, "Id", "Id", songsModel.GenreID);
            //ViewData["PerformerID"] = new SelectList(_context.Performers, "Id", "Id", songsModel.PerformerID);
            return View(songsModel);
        }

        // GET: SongsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songsModel = await repo.GetSong(id.Value);
            if (songsModel == null)
            {
                return NotFound();
            }
            //ViewData["GenreID"] = new SelectList(_context.Genres, "Id", "Id", songsModel.GenreID);
            //ViewData["PerformerID"] = new SelectList(_context.Performers, "Id", "Id", songsModel.PerformerID);
            return View(songsModel);
        }

        // POST: SongsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenreID,PerformerID,Track")] SongsModel songsModel)
        {
            if (id != songsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.UpdateSong(songsModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongsModelExists(songsModel.Id))
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
            //ViewData["GenreID"] = new SelectList(_context.Genres, "Id", "Id", songsModel.GenreID);
            //ViewData["PerformerID"] = new SelectList(_context.Performers, "Id", "Id", songsModel.PerformerID);
            return View(songsModel);
        }

        // GET: SongsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songsModel = await repo.GetSong(id.Value);
            if (songsModel == null)
            {
                return NotFound();
            }

            return View(songsModel);
        }

        // POST: SongsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songsModel = await repo.GetSong(id);
            if (songsModel != null)
            {
                repo.DeleteSong(songsModel.Id);
            }

            await repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool SongsModelExists(int id)
        {
            return repo.SongsModelExists(id);
        }
    }
}
