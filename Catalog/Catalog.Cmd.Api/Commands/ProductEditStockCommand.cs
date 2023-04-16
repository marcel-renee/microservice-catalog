using CQRS.Core.Commands;

namespace Catalog.Cmd.Api.Commands
{
    public class ProductEditStockCommand : BaseCommand
    {
        public int Stock { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
