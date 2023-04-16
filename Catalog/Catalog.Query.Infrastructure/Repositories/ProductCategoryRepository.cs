using Catalog.Query.Domain.Entities;
using Catalog.Query.Domain.Repositories;
using Catalog.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Query.Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DataBaseContextFactory _contextFactory;
        public ProductCategoryRepository(DataBaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(ProductCategoryEntity category)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            context.Categories.Add(category);
            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid categoryId)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            var category = await GetByIdAsync(categoryId);
            context.Categories.Remove(category);
            _ = await context.SaveChangesAsync();
        }

        public async Task<ProductCategoryEntity> GetByIdAsync(Guid categoryId)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            return _ = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<List<ProductCategoryEntity>> ListAllAsync()
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            return _ = await context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(ProductCategoryEntity category)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            context.Categories.Update(category);
            _ = await context.SaveChangesAsync();
        }
    }
}
