using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Common.Events
{
    public class ProductCreateEvent : BaseEvent
    {
        public ProductCreateEvent() : base(nameof(ProductCreateEvent))
        { }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime ModificationDate { get; set; }

    }
}
