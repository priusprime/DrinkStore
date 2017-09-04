using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Models
{
    public class ShoppingCart
    {
        private readonly DrinkStoreContext DbContext;
        private readonly string ShoppingCartId;

        private ShoppingCart(DrinkStoreContext dbContext,string id)
        {
            DbContext = dbContext;
            ShoppingCartId = id;
        }

        public static ShoppingCart GetCart(DrinkStoreContext db, HttpContext context)
            => GetCart(db, GetCartId(context));

        public static ShoppingCart GetCart(DrinkStoreContext db, string cartId)
            => new ShoppingCart(db, cartId);

        private static string GetCartId(HttpContext context)
        {
            var cartId = context.Session.GetString("Session");

            if (cartId == null)
            {
                //A GUID to hold the cartId. 
                cartId = Guid.NewGuid().ToString();

                // Send cart Id as a cookie to the client.
                context.Session.SetString("Session", cartId);
            }

            return cartId;
        }

        public Task<List<CartItem>> GetCartItems()
        {
            return DbContext
                .CartItems
                .Where(c => c.CartId == ShoppingCartId)
                .Include(c => c.Drink)
                .ToListAsync();
        }

        public Task<List<string>> GetCartDrinkTitles()
        {
            return DbContext
                .CartItems
                .Where(c => c.CartId == ShoppingCartId)
                .Select(c => c.Drink.Title)
                .OrderBy(n => n)
                .ToListAsync();
        }

        public async Task AddToCart(Drink drink)
        {
            //Get the matching cart and drink instances
            var cartItem = await DbContext.CartItems.
                SingleOrDefaultAsync(c => c.CartId == ShoppingCartId && c.DrinkID == drink.DrinkID);

            if(cartItem==null)
            {
                //Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    CartId = ShoppingCartId,
                    DrinkID = drink.DrinkID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                DbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
        }

        public Task<decimal> GetTotal()
        {
            return DbContext
                .CartItems
                .Include(c => c.Drink)
                .Where(c => c.CartId == ShoppingCartId)
                .Select(c => c.Drink.Price * c.Count)
                .SumAsync();
        }

        public Task<int> GetCount()
        {
            return DbContext
                .CartItems
                .Where(c => c.CartId == ShoppingCartId)
                .Select(c => c.Count)
                .SumAsync();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = DbContext.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId 
                && c.CartItemId == id);

            int itemCount = 0;

            if(cartItem!=null)
            {
                if(cartItem.Count>1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    DbContext.CartItems.Remove(cartItem);
                }
            }
            return itemCount;
        }

        public async Task<int> CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = await GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                //var album = _db.Albums.Find(item.AlbumId);
                var drink = await DbContext.Drinks.SingleAsync(a => a.DrinkID == item.DrinkID);

                var orderDetail = new OrderDetail
                {
                    DrinkId = item.DrinkID,
                    OrderId = order.OrderId,
                    UnitPrice = drink.Price,
                    Quantity = item.Count,
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * drink.Price);

                DbContext.OrderDetails.Add(orderDetail);
            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Empty the shopping cart
            await EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        public async Task EmptyCart()
        {
            var cartItems = await DbContext
                .CartItems
                .Where(c => c.CartId == ShoppingCartId)
                .ToArrayAsync();

            DbContext.CartItems.RemoveRange(cartItems);
        }
    }
}
