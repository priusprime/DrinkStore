using DrinkStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Components
{
    [ViewComponent(Name ="CartInfo")]
    public class CartInfoComponent:ViewComponent
    {
        public CartInfoComponent(DrinkStoreContext dbContext)
        {
            DbContext = dbContext;
        }

        private DrinkStoreContext DbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = ShoppingCart.GetCart(DbContext, HttpContext);

            var cartItems = await cart.GetCartDrinkTitles();

            ViewBag.CartCount = cartItems.Count;
            ViewBag.CartSummary = string.Join("\n",cartItems.Distinct());

            return View();
        }
    }
}
