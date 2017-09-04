using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkStore.Models;

namespace DrinkStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly DrinkStoreContext _context;

        public StoreController(DrinkStoreContext context)
        {
            _context = context;    
        }

        // GET: Store
        public async Task<IActionResult> Index()
        {
            return View(await _context.Styles.ToListAsync());
        }

        public async Task<IActionResult> Browse(string style)
        {
            var styleModel = await _context.Styles
                .Where(s => s.Title == style)
                .Include(s => s.Drinks)
                .FirstOrDefaultAsync();

            if (styleModel == null)
            {
                return NotFound();
            }

            return View(styleModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var drink = await _context.Drinks
                .Where(d => d.DrinkID == id)
                .Include(d=>d.Style)
                .FirstOrDefaultAsync();

            if(drink==null)
            {
                return NotFound();
            }

            return View(drink);
        }
    }
}
