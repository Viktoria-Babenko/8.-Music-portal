using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _8._Music_portal.Models;
using _8._Music_portal.NewFolder;
using _8._Music_portal.Repository;

namespace _8._Music_portal.Controllers
{
    public class SongsController : Controller
    {
        IRepository repo;
        IWebHostEnvironment _appEnvironment;

        public SongsController(IRepository r, IWebHostEnvironment a)
        {
            repo = r;
            _appEnvironment = a;
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
        public async Task<IActionResult> Create()
        {
            ViewData["GenreID"] = repo.GetGenres();
            ViewData["PerformerID"] = repo.GetPerformers();
            return View();
        }

        // POST: SongsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GenreID,PerformerID,Track")] SongsModel songsModel, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Songs/" + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                songsModel.Track = path;
                var songs = await repo.SongsCreate(songsModel);
                if (songs != null)
                {
                    if (songs.Name == songsModel.Name && songs.Performer == songsModel.Performer || songs.Genre == songsModel.Genre)
                    {
                        ModelState.AddModelError("", "Такой трек уже есть в списке!");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Вы не выбрали файл трека !");
            }
            if (ModelState.IsValid)
            {
                await repo.CreateSong(songsModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreID"] = repo.GetGenres(songsModel);
            ViewData["PerformerID"] = repo.GetPerformers(songsModel);
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
            ViewData["GenreID"] = repo.GetGenres(songsModel);
            ViewData["PerformerID"] = repo.GetPerformers(songsModel);
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
            ViewData["GenreID"] = repo.GetGenres(songsModel);
            ViewData["PerformerID"] = repo.GetPerformers(songsModel);
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
