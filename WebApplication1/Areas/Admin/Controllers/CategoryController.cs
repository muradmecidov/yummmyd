using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Admin.ViewModels;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Category> categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                return View(category);
            }

            bool isExists = await _context.Categories.AnyAsync(c =>
            c.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            if (isExists)
            {
                ModelState.AddModelError("Name", "Category name already exists");
                return View(category);
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            Category? category = _context.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int Id)
        {
            Category? category = _context.Categories.Find(Id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            Category? editedCategory = _context.Categories.Find(category.Id);
            if (editedCategory == null)
            {
                return NotFound();
            }
            editedCategory.Name = category.Name;
            _context.Categories.Update(editedCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }








    }
}
