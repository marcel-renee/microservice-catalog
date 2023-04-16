using Catalog.Common.Events;
using Catalog.Query.Domain.Entities;
using Catalog.Query.Domain.Repositories;
using Catalog.Query.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Query.Infrastructure.Handlers
{
    public class CatalogEventHandler : ICatalogEventHandler
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public CatalogEventHandler(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }


        public async Task On(ProductCreateEvent @event)
        {
            var product = new ProductEntity()
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                CategoryId = @event.CategoryId,
                Active = @event.Active,
                ModificationDate = @event.ModificationDate,
                Stock = @event.Stock,
                Value = @event.Value,
            };
            await _productRepository.CreateAsync(product);
        }

        public async Task On(ProductDeleteEvent @event)
        {
            await _productRepository.DeleteAsync(@event.Id);
        }

        public async Task On(ProductEditNameDescriptionEvent @event)
        {
            var product = await _productRepository.GetByIdAsync(@event.Id);
            product.Name = @event.Name;
            product.Description = @event.Description;
            product.ModificationDate = @event.ModificationDate;
            await _productRepository.UpdateAsync(product);
        }

        public async Task On(ProductEditValueEvent @event)
        {
            var product = await _productRepository.GetByIdAsync(@event.Id);
            product.Value = @event.Value;
            product.ModificationDate = @event.ModificationDate;
            await _productRepository.UpdateAsync(product);
        }


        public async Task On(ProductEditStockEvent @event)
        {
            var product = await _productRepository.GetByIdAsync(@event.Id);
            product.Stock = @event.Stock;
            product.ModificationDate = @event.ModificationDate;
            await _productRepository.UpdateAsync(product);
        }

        public async Task On(ProductChangeCategoryEvent @event)
        {
            var product = await _productRepository.GetByIdAsync(@event.Id);
            product.CategoryId = @event.CategoryId;
            product.ModificationDate = @event.ModificationDate;
            await _productRepository.UpdateAsync(product);
        }

        public async Task On(ProductCategoryCreateEvent @event)
        {
            var category = new ProductCategoryEntity()
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
            };
            await _productCategoryRepository.CreateAsync(category);
        }
    }
}
