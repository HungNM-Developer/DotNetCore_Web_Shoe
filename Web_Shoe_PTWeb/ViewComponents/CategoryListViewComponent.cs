using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly Web_Shoe_PTWebContext _context;
        public CategoryListViewComponent(Web_Shoe_PTWebContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> catList = await _context.Categories.ToListAsync();
            return View(catList);
        }
    }
}
