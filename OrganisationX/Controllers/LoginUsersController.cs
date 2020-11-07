using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrganisationX;

namespace OrganisationX.Controllers
{
    public class LoginUsersController : Controller
    {
        private readonly DimensionDContext _context;

        public LoginUsersController(DimensionDContext context)
        {
            _context = context;
        }

        // GET: LoginUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoginUser.ToListAsync());
        }

        // GET: LoginUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.LoginUser
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (loginUser == null)
            {
                return NotFound();
            }

            return View(loginUser);
        }

        // GET: LoginUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Empid,Fname,Lname,Email,Password")] LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginUser);
        }

        // GET: LoginUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.LoginUser.FindAsync(id);
            if (loginUser == null)
            {
                return NotFound();
            }
            return View(loginUser);
        }

        // POST: LoginUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Empid,Fname,Lname,Email,Password")] LoginUser loginUser)
        {
            if (id != loginUser.Empid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginUserExists(loginUser.Empid))
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
            return View(loginUser);
        }

        // GET: LoginUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUser = await _context.LoginUser
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (loginUser == null)
            {
                return NotFound();
            }

            return View(loginUser);
        }

        // POST: LoginUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loginUser = await _context.LoginUser.FindAsync(id);
            _context.LoginUser.Remove(loginUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginUserExists(string id)
        {
            return _context.LoginUser.Any(e => e.Empid == id);
        }
    }
}
