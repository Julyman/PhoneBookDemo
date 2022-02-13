using Dme.PhoneBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookMvcApp.Controllers
{
    public class UsersController : Controller
    {
        #region Fields
        private readonly IUserRepository _context;
        #endregion

        #region Ctor
        public UsersController(IUserRepository context)
        {
            _context = context;
        }
        #endregion

        #region CRUD Action
        // GET: Demo
        public async Task<IActionResult> Index(int page = 1)
        {
            var content = await _context.GetPageAsync(page);
            return View(content);
        }

        // GET: [controller]/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return BadRequest();

            var user = await _context.GetUserAsync(id.Value);
            if (user == null)
                return NotFound();

            ViewBag.BRoute = Request.Headers["Referer"].ToString();
            return View(user);
        }

        // GET: [controller]/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: [controller]/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind("Id,FirstName,LastName,Dob,PictureThumbnail")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.InsertAsync(user);
                if (result == null)
                    return BadRequest();
                else
                    return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: [controller]/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return BadRequest();

            var user = await _context.GetUserAsync(id.Value);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: [controller]/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FirstName,LastName,Dob,PictureThumbnail")] User user)
        {
            if (id != user.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = await _context.UpdateAsync(user);
                if (result == null)
                    return NotFound();
                else
                    return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Demo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.GetUserAsync(id.Value);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
