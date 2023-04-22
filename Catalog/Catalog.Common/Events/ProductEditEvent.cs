using CQRS.Core.Commands;
using CQRS.Core.Events;

namespace Catalog.Common.Events
{
    public class ProductEditEvent : BaseEvent
    {
        public ProductEditEvent() : base(nameof(ProductEditEvent))
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
