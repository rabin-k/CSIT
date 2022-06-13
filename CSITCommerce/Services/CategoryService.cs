using CSITCommerce.Models;

namespace CSITCommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CommerceDbContext _commerceDbContext;

        public CategoryService(CommerceDbContext commerceDbContext)
        {
            _commerceDbContext = commerceDbContext;
        }

        public void Add(CategoryModel model)
        {
            _commerceDbContext.Categories.Add(model);
            _commerceDbContext.SaveChanges();
        }
    }
}
