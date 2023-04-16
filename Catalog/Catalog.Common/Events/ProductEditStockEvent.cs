using CQRS.Core.Commands;
using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductEditStockEvent : BaseEvent
    {
        public ProductEditStockEvent() : base(nameof(ProductEditStockEvent))
        {
        }

        public int Stock { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
