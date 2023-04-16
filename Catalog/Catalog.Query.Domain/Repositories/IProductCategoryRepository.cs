using Catalog.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Query.Domain.Repositories
{
    public interface IProductCategoryRepository
    {
        Task CreateAsync(ProductCategoryEntity category);
        Task UpdateAsync(ProductCategoryEntity category);
        Task DeleteAsync(Guid categoryId);
        Task<ProductCategoryEntity> GetByIdAsync(Guid categoryId);
        Task<List<ProductCategoryEntity>> ListAllAsync();
    }
}
