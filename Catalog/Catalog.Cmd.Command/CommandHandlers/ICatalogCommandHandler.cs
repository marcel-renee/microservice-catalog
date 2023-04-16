using Catalog.Cmd.Aplication.Commands;

namespace Catalog.Cmd.Aplication.CommandHandlers
{
    public interface ICatalogCommandHandler
    {
        Task HandleAsync(ProductCreateCommand command);
        Task HandleAsync(ProductDeleteCommand command);
        Task HandleAsync(ProductEditNameDescriptionCommand command);
        Task HandleAsync(ProductEditStockCommand command);
        Task HandleAsync(ProductEditValueCommand command);
        Task HandleAsync(ProductChangeCategoryCommand command);
        Task HandleAsync(ProductCategoryCreateCommand command);
    }
}
