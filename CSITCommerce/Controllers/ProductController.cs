using CSITCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSITCommerce.Controllers
{
    [Authorize(Roles = "Store Admin, Super Admin")]
    public class ProductController : Controller
    {
        private readonly CommerceDbContext _commerceDbContext;
        public ProductController(CommerceDbContext commerceDbContext)
        {
            _commerceDbContext = commerceDbContext;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> products = (from p in _commerceDbContext.Products
                                               join c in _commerceDbContext.Categories on p.CategoryId equals c.Id
                                               //where p.IsActive == true
                                               select new ProductViewModel
                                               {
                                                   Name = p.Name,
                                                   Id = p.Id,
                                                   IsActive = p.IsActive,
                                                   CategoryId = p.CategoryId,
                                                   Description = p.Description,
                                                   Photo = p.Photo,
                                                   Price = p.Price,
                                                   Sku = p.Sku,
                                                   CategoryName = c.Name
                                               }).ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<CategoryModel> list = _commerceDbContext.Categories.Where(x => x.IsActive.HasValue ? x.IsActive.Value : false).ToList();
            List<SelectListItem> selectList = list
                                            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                                            .ToList();
            ViewBag.Categories = selectList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            _commerceDbContext.Products.Add(model);
            _commerceDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
