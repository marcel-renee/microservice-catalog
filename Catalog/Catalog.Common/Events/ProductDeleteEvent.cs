using CQRS.Core.Commands;
using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductDeleteEvent : BaseEvent
    {
        public ProductDeleteEvent() : base(nameof(ProductDeleteEvent))
        {
        }        
    }
}
