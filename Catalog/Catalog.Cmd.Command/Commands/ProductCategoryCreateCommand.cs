using CQRS.Core.Commands;

namespace Catalog.Cmd.Command.Commands
{
    public class ProductCategoryCreateCommand : BaseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
