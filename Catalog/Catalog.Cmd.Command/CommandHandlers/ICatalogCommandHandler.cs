using Catalog.Cmd.Command.Commands;

namespace Catalog.Cmd.Command.CommandHandlers
{
    public interface ICatalogCommandHandler
    {
        Task HandleAsync(ProductCreateCommand command);
        Task HandleAsync(ProductDeleteCommand command);
        Task HandleAsync(ProductEditCommand command);
        Task HandleAsync(ProductEditStockCommand command);
        Task HandleAsync(ProductEditValueCommand command);        
        Task HandleAsync(ProductCategoryCreateCommand command);
    }
}
