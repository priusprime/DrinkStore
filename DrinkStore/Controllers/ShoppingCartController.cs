using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkStore.Models;
using DrinkStore.ViewModels;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace DrinkStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;

        private readonly DrinkStoreContext _context;

        public ShoppingCartController(DrinkStoreContext context, ILogger<ShoppingCartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            var cart = ShoppingCart.GetCart(_context, HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems =await cart.GetCartItems(),
                CartTotal =await cart.GetTotal()
            };

            return View(viewModel);
        }
      
        public async Task<IActionResult> AddToCart(int id)
        {
            //Retrieve drink from db
            var addedDrink = await _context.Drinks.SingleOrDefaultAsync(d => d.DrinkID == id);

            var cart = ShoppingCart.GetCart(_context, HttpContext);
            await cart.AddToCart(addedDrink);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
             
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int id,CancellationToken requestAborted)
        {
            //Retrieve the current users' shopping cart
            var cart = ShoppingCart.GetCart(_context, HttpContext);

            //Get the name of the drink to display confirmation
            var cartItem = await _context.CartItems
                .Where(c => c.CartItemId == id)
                .Include(c => c.Drink)
                .SingleOrDefaultAsync();

            string message;
            int itemCount;
            if (cartItem != null)
            {
                //Remove from cart
                itemCount = cart.RemoveFromCart(id);
                await _context.SaveChangesAsync(requestAborted);

                string removed = (itemCount > 0) ? "1 copy of" : string.Empty;
                message = removed + cartItem.Drink.Title + "has been removed from your shopping cart.";
            }
            else
            {
                itemCount = 0;
                message = "Could not find this item,nothing has been removed from your shopping cart.";
            }

            //Display the confirmation message

            var results = new ShoppingCartRemoveViewModel
            {
                Message = message,
                CartTotal = await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            _logger.LogInformation("Drink {id} was removed from a cart.", id);

            return Json(results);
        }

        // GET: ShoppingCart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Drink)
                .SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(m => m.CartItemId == id);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
