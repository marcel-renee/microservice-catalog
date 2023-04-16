using CQRS.Core.Commands;

namespace Catalog.Cmd.Command.Commands
{
    public class ProductCreateCommand : BaseCommand
    {
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime ModificationDate { get; set; }
        
    }
}
