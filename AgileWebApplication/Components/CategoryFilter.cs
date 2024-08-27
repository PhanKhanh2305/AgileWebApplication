using AgileWebApplication.Areas.Identity.Data;
using AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AgileWebApplication.Components
{
    public class CategoryFilter : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CategoryFilter(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View("Index", _context.Categories.ToList());
        }
    }
}
