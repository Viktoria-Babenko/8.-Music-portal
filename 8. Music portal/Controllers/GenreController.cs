using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _8._Music_portal.Models;
using _8._Music_portal.Repository;

namespace _8._Music_portal.Controllers
{
    public class GenreController : Controller
    {
        IRepository<GenreModel> repo;

        public GenreController(IRepository<GenreModel> r)
        {
            repo = r;
        }

        // GET: Genre
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAll());
        }

        // GET: Genre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreModel = await repo.Get(id.Value);
            if (genreModel == null)
            {
                return NotFound();
            }

            return View(genreModel);
        }

        // GET: Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GenreModel genreModel)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(genreModel);
                return RedirectToAction(nameof(Index));
            }
            return View(genreModel);
        }

        // GET: Genre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreModel = await repo.Get(id.Value);
            if (genreModel == null)
            {
                return NotFound();
            }
            return View(genreModel);
        }

        // POST: Genre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GenreModel genreModel)
        {
            if (id != genreModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(genreModel);
                    await repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreModelExists(genreModel.Id))
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
            return View(genreModel);
        }

        // GET: Genre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreModel = await repo.Get(id.Value);
            if (genreModel == null)
            {
                return NotFound();
            }

            return View(genreModel);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreModel = await repo.Get(id);
            if (genreModel != null)
            {
                await repo.Delete(id);

            }
            return RedirectToAction(nameof(Index));
        }

        private bool GenreModelExists(int id)
        {
            return repo.ModelExists(id);
        }
    }
}
