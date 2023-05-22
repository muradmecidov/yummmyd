using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Admin.ViewModels;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Product> products = await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Product product = new Product
                {
                    Name = model.Name,
                    Ingredients = model.Ingredients,
                    Price = model.Price,
                    ImagePath = uniqueFileName
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
