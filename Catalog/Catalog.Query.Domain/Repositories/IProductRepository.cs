using Catalog.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Query.Domain.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductEntity product);
        Task UpdateAsync(ProductEntity product);
        Task DeleteAsync(Guid productId);
        Task<ProductEntity> GetByIdAsync(Guid productId);
        Task<List<ProductEntity>> ListAllAsync(bool active);
        Task<List<ProductEntity>> ListByCategoryAsync(Guid categoryId);
    }
}
