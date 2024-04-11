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
    public class UserController : Controller
    {
        IRepository repo;

        public UserController(IRepository r)
        {
            repo = r;
        }

        // GET: UserModels
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAllUser());
        }

        // GET: UserModels/Activate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await repo.GetUser(id.Value);
            if (userModel == null)
            {
                return NotFound();
            }
            userModel.Status = true;
            repo.UpdateUser(userModel);
            await repo.Save();
            return View(userModel);
        }
        
        // GET: UserModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Login,Password,Salt,email,Status")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                await repo.CreateUser(userModel);
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await repo.GetUser(id.Value);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await repo.GetUser(id);
            if (userModel != null)
            {
                repo?.DeleteUser(userModel.Id);
            }

            await repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
            return repo.UserModelExists(id);
        }
    }
}
