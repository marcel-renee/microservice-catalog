using CQRS.Core.Commands;

namespace Catalog.Cmd.Command.Commands
{
    public class ProductEditValueCommand : BaseCommand
    {
        public decimal Value { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
