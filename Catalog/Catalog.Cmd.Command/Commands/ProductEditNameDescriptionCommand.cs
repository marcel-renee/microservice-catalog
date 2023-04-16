using CQRS.Core.Commands;

namespace Catalog.Cmd.Aplication.Commands
{
    public class ProductEditNameDescriptionCommand : BaseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModificationDate { get; set; }

    }
}
