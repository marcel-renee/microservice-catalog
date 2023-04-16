using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductCategoryCreateEvent : BaseEvent
    {
        public ProductCategoryCreateEvent() : base(nameof(ProductCategoryCreateEvent))
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
