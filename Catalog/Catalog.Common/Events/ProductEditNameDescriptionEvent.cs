using CQRS.Core.Commands;
using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductEditNameDescriptionEvent : BaseEvent
    {
        public ProductEditNameDescriptionEvent() : base(nameof(ProductEditNameDescriptionEvent))
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
