using Catalog.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Query.Infrastructure.Handlers
{
    public interface ICatalogEventHandler
    {
        Task On(ProductCreateEvent @event);
        Task On(ProductDeleteEvent @event);
        Task On(ProductEditEvent @event);
        Task On(ProductEditValueEvent @event);
        Task On(ProductEditStockEvent @event);        
        Task On(ProductCategoryCreateEvent @event);

    }
}
