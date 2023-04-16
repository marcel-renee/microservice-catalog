using CQRS.Core.Commands;

namespace Catalog.Cmd.Aplication.Commands
{
    public class ProductChangeCategoryCommand : BaseCommand
    {
        public Guid CategoryId { get; set; }

    }
}
