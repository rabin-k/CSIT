using CSITCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSITCommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryapiController : ControllerBase
    {
        private readonly CommerceDbContext _commerceDbContext;

        public CategoryapiController(CommerceDbContext commerceDbContext)
        {
            _commerceDbContext = commerceDbContext;
        }

        [HttpGet]
        public List<CategoryModel> Get()
        {
            List<CategoryModel> categories = _commerceDbContext.Categories.ToList();

            return categories;
        }

        [HttpPost]
        public IActionResult Post(CategoryModel model)
        {
            _commerceDbContext.Categories.Add(model);
            _commerceDbContext.SaveChanges();
            return Ok();
        }
    }
}
