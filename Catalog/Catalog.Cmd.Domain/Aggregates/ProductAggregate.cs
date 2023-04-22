using Catalog.Common.Events;
using CQRS.Core.Domain;

namespace Catalog.Cmd.Domain.Aggregates
{
    public class ProductAggregate : AggregateRoot
    {
        private bool _active;

        public bool Active { get => _active; set => _active = value; }

        public ProductAggregate()
        {

        }

        public ProductAggregate(Guid id)
        {
            _id = id;
        }

        public ProductAggregate(Guid id, string name, string description, decimal value, int stock, Guid categoryId)
        {
            RaiseEvent(new ProductCreateEvent()
            {
                Id = id,
                Name = name,
                Description = description,
                Value = value,
                Stock = stock,
                ModificationDate = DateTime.Now,
                CategoryId = categoryId,
                Active = true
            });
        }

        public void Apply(ProductCreateEvent @event)
        {
            _id = @event.Id;
            _active = true;
        }

        public void EditProduct(string name, string description, Guid CategoryId)
        {
            if (!_active)
                throw new InvalidOperationException("You canot change the name of an inactive Product");
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException($"The value of {nameof(name)} name canot be null or empty. Please provide a valid {nameof(name)}");
            if (string.IsNullOrWhiteSpace(description))
                throw new InvalidOperationException($"The value of {nameof(description)} name canot be null or empty. Please provide a valid {nameof(description)}");

            RaiseEvent(new ProductEditEvent()
            {
                Id = _id,
                Name = name,
                Description = description,
                ModificationDate = DateTime.Now,
                CategoryId = CategoryId
            });
        }

        public void Apply(ProductEditEvent @event)
        {
            _id = @event.Id;
        }

        public void EditValue(decimal value)
        {
            if (!_active)
                throw new InvalidOperationException("You canot change the Value of an inactive Product");

            if (value < 0)
                throw new ArgumentOutOfRangeException("The Value must be higher or equal to 0");

            RaiseEvent(new ProductEditValueEvent()
            {
                Id = _id,
                Value = value,
                ModificationDate = DateTime.Now
            });
        }

        public void Apply(ProductEditValueEvent @event)
        {
            _id = @event.Id;
        }


        public void EditStock(int stock)
        {
            if (!_active)
                throw new InvalidOperationException("You canot change the stock of an inactive Product");

            if (stock < 0)
                throw new ArgumentOutOfRangeException("The Stock must be higher or equal to 0");

            RaiseEvent(new ProductEditStockEvent()
            {
                Id = _id,
                Stock = stock,
                ModificationDate = DateTime.Now
            });
        }

        public void Apply(ProductEditStockEvent @event)
        {
            _id = @event.Id;
        }

        public void Delete()
        {
            RaiseEvent(new ProductDeleteEvent()
            {
                Id = _id
            });
        }

        public void Apply(ProductDeleteEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }
        public void CreateCategory(Guid id, string categoryName, string categoryDescription)
        {
            RaiseEvent(new ProductCategoryCreateEvent()
            {
                Id = id,
                Name = categoryName,
                Description = categoryDescription
            });
        }

        public void Apply(ProductCategoryCreateEvent @event)
        {
            _id = @event.Id;
            _active = true;
        }

    }
}
