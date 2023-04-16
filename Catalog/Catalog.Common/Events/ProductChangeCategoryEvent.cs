using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductChangeCategoryEvent : BaseEvent
    {
        public ProductChangeCategoryEvent() : base(nameof(ProductChangeCategoryEvent))
        {
        }

        public Guid CategoryId { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
