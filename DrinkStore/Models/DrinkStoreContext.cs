using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkStore.Models;

namespace DrinkStore.Models
{
    public class ApplicationUser : IdentityUser{}

    public class DrinkStoreContext : IdentityDbContext<ApplicationUser>
    {
        public DrinkStoreContext(DbContextOptions<DrinkStoreContext> options) : base(options)
        {
            
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
