using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductEditValueEvent : BaseEvent
    {
        public ProductEditValueEvent() : base(nameof(ProductEditValueEvent))
        {
        }

        public decimal Value { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
