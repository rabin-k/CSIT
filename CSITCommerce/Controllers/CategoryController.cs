using CSITCommerce.Models;
using CSITCommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CSITCommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CommerceDbContext _commerceDbContext;
        private readonly ICategoryService _categoryService;

        public CategoryController(CommerceDbContext commerceDbContext, ICategoryService categoryService)
        {
            _commerceDbContext = commerceDbContext;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            List<CategoryModel> categories = _commerceDbContext.Categories.ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(model);
                return RedirectToAction("Create");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CategoryModel c = _commerceDbContext.Categories.FirstOrDefault(X=>X.Id == id);

            if(c == null)
                c = new CategoryModel();

            return View(c);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel model)
        {
            _commerceDbContext.Categories.Update(model);
            _commerceDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CategoryModel c = _commerceDbContext.Categories.FirstOrDefault(X => X.Id == id);
            if (c != null)
            {
                _commerceDbContext.Categories.Remove(c);
                _commerceDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
