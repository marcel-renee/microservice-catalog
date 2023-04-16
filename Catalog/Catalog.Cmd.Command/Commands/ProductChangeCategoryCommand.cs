using CQRS.Core.Commands;

namespace Catalog.Cmd.Command.Commands
{
    public class ProductChangeCategoryCommand : BaseCommand
    {
        public Guid CategoryId { get; set; }

    }
}
