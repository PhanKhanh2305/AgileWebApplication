using AgileWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AgileWebApplication.Components
{
    public class ProductList : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ProductList(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View("Index",_context.Products.ToList());
        }
    }
}
