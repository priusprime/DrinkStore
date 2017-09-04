using DrinkStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Components
{
    [ViewComponent(Name ="StyleMenu")]
    public class StyleMenuComponent:ViewComponent
    {
        public StyleMenuComponent(DrinkStoreContext dbContext)
        {
            DbContext = dbContext;
        }

        private DrinkStoreContext DbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var style = await DbContext.Styles.Select(s => s.Title).Take(5).ToListAsync();

            return View(style);
        }
    }
}
