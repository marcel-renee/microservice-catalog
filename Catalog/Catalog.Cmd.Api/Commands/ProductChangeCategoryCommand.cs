using CQRS.Core.Commands;

namespace Catalog.Cmd.Api.Commands
{
    public class ProductChangeCategoryCommand : BaseCommand
    {
        public Guid CategoryId { get; set; }

    }
}
