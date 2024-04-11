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
    public class PerformerController : Controller
    {
        IRepository<PerformerModel> repo;

        public PerformerController(IRepository<PerformerModel> r)
        {
            repo = r;
        }

        // GET: Performer
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAll());
        }

        // GET: Performer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerModel = await repo.Get(id.Value);
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
                await repo.Create(performerModel);
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

            var performerModel = await repo.Get(id.Value);
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
                    repo.Update(performerModel);
                    await repo.Save();
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

            var performerModel = await repo.Get(id.Value);
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
            var performerModel = await repo.Get(id);
            if (performerModel != null)
            {
                await repo.Delete(performerModel.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerModelExists(int id)
        {
            return repo.ModelExists(id);
        }
    }
}
