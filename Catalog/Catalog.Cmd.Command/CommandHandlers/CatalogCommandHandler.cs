using Catalog.Cmd.Command.Commands;
using Catalog.Cmd.Domain.Aggregates;
using Catalog.Common.Events;
using CQRS.Core.Handlers;

namespace Catalog.Cmd.Command.CommandHandlers
{
    public class CatalogCommandHandler : ICatalogCommandHandler
    {
        private readonly IEventSourcingHandler<ProductAggregate> _eventSourcingHandler;

        public CatalogCommandHandler(IEventSourcingHandler<ProductAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }
        public async Task HandleAsync(ProductCreateCommand command)
        {
            var aggregate = new ProductAggregate(command.Id, command.Name, command.Description, command.Value, command.Stock, command.CategoryId);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(ProductDeleteCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.Delete();
            await _eventSourcingHandler.SaveAsync(aggregate);
        }


        public async Task HandleAsync(ProductEditCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditProduct(command.Name,command.Description, command.CategoryId);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(ProductEditStockCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditStock(command.Stock);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(ProductEditValueCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditValue(command.Value);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(ProductCategoryCreateCommand command)
        {
            var aggregate = new ProductAggregate(command.Id);
            aggregate.CreateCategory(command.Id, command.Name, command.Description);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}
