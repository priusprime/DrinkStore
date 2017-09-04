using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace DrinkStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("ManageStore")]
    public class DrinksController : Controller
    {
        private readonly DrinkStoreContext _context;

        public DrinksController(DrinkStoreContext context)
        {
            _context = context;    
        }

        // GET: Admin/Drinks
        public async Task<IActionResult> Index()
        {
            var drinkStoreContext = _context.Drinks.Include(d => d.Style);
            return View(await drinkStoreContext.ToListAsync());
        }

        // GET: Admin/Drinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.Style)
                .SingleOrDefaultAsync(m => m.DrinkID == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Admin/Drinks/Create
        public IActionResult Create()
        {
            ViewData["StyleID"] = new SelectList(_context.Styles, "StyleID", "Title");
            return View();
        }

        // POST: Admin/Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrinkID,Title,StyleID,Price,DrinkArtUrl")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["StyleID"] = new SelectList(_context.Styles, "StyleID", "Title", drink.StyleID);
            return View(drink);
        }

        // GET: Admin/Drinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.SingleOrDefaultAsync(m => m.DrinkID == id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewData["StyleID"] = new SelectList(_context.Styles, "StyleID", "Title", drink.StyleID);
            return View(drink);
        }

        // POST: Admin/Drinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrinkID,Title,StyleID,Price,DrinkArtUrl")] Drink drink)
        {
            if (id != drink.DrinkID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.DrinkID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["StyleID"] = new SelectList(_context.Styles, "StyleID", "Title", drink.StyleID);
            return View(drink);
        }

        // GET: Admin/Drinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .Include(d => d.Style)
                .SingleOrDefaultAsync(m => m.DrinkID == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Admin/Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.SingleOrDefaultAsync(m => m.DrinkID == id);
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.DrinkID == id);
        }
    }
}
