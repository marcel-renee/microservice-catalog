using CQRS.Core.Commands;

namespace Catalog.Cmd.Command.Commands
{
    public class ProductEditStockCommand : BaseCommand
    {
        public int Stock { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
