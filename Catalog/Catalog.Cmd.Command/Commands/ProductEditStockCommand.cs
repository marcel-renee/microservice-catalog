using CQRS.Core.Commands;

namespace Catalog.Cmd.Aplication.Commands
{
    public class ProductEditStockCommand : BaseCommand
    {
        public int Stock { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
