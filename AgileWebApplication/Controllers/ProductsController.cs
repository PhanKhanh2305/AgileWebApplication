using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgileWebApplication.Areas.Identity.Data;
using AgileWebApplication.Models;
using AgileWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AgileWebApplication.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int PageSize = 12;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products

        public async Task<IActionResult> Index(int productpage = 1)
        {
            return View(new ProductListViewModel
            {
                Products = _context.Products.Skip((productpage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productpage,
                    TotalItem = _context.Products.Count()
                }

            });
        }
        [HttpPost]
        public async Task<IActionResult> Search(string keywords, int productpage = 1)
        {
            return View("Index", new ProductListViewModel
            {
                Products = _context.Products
                .Where(p => p.ProductName.Contains(keywords))
                .Skip((productpage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productpage,
                    TotalItem = _context.Products.Count()
                }

            });
        }
        [HttpGet]
        public async Task<IActionResult> ProductsByCat(int categoryId, int productpage = 1)
        {
            return View("Index", new ProductListViewModel
            {
                Products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Skip((productpage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productpage,
                    TotalItem = _context.Products.Count()
                }

            });
        }
        [HttpGet]
        public async Task<IActionResult> ProductsByGender(int genderId, int productpage = 1)
        {
            return View("Index", new ProductListViewModel
            {
                Products = _context.Products
                .Where(p => p.GenderId == genderId)
                .Include(p => p.Gender)
                .Skip((productpage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productpage,
                    TotalItem = _context.Products.Count()
                }

            });
        }


        // GET: Products/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize (Roles =("Admin"))]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["StyleId"] = new SelectList(_context.Genders, "StyleId", "StyleName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,ProductPhoto,ProductPrice,BestSeller,CategoryId,StyleId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["StyleId"] = new SelectList(_context.Genders, "StyleId", "StyleName", product.GenderId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["StyleId"] = new SelectList(_context.Genders, "StyleId", "StyleName", product.GenderId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,ProductPhoto,ProductPrice,BestSeller,CategoryId,StyleId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["StyleId"] = new SelectList(_context.Genders, "StyleId", "StyleName", product.GenderId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
