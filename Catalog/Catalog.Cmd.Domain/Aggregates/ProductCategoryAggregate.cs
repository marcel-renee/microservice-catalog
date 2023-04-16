using Catalog.Common.Events;
using CQRS.Core.Domain;

namespace Catalog.Cmd.Domain.Aggregates
{
    public class ProductCategoryAggregate : AggregateRoot
    {
        private bool _active;

        public bool Active { get => _active; set => _active = value; }

        public ProductCategoryAggregate()
        {

        }

        public ProductCategoryAggregate(Guid id)
        {
            _id = id;
        }

        public ProductCategoryAggregate(Guid id, string categoryName, string categoryDescription)
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
