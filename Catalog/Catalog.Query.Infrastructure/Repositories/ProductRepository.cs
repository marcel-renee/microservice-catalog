using Catalog.Query.Domain.Entities;
using Catalog.Query.Domain.Repositories;
using Catalog.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Catalog.Query.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContextFactory _contextFactory;
        public ProductRepository(DataBaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(ProductEntity product)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            context.Products.Add(product);
            _ = await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            context.Products.Update(product);
            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            var product = await GetByIdAsync(productId);
            product.Active = false;
            context.Products.Update(product);
            //context.Products.Remove(product);
            _ = await context.SaveChangesAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(Guid productId)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            return _ = await context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<List<ProductEntity>> ListAllAsync(bool active)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            return _ = await context.Products
                .Where(x => x.Active == active)
                .AsNoTracking()
                .Include(x => x.Category).AsNoTracking()
                .ToListAsync();
        }


        public async Task<List<ProductEntity>> ListByCategoryAsync(Guid categoryId)
        {
            using DataBaseContext context = _contextFactory.CreateDbContext();
            return _ = await context.Products
                .AsNoTracking()
                .Include(x => x.Category).AsNoTracking()
                .Where(x => x.Category.Id == categoryId)
                .ToListAsync();
        }


    }
}
