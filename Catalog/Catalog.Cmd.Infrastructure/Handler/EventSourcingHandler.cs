using Catalog.Cmd.Domain.Aggregates;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Cmd.Infrastructure.Handler
{
    public class EventSourcingHandler : IEventSourcingHandler<ProductAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ProductAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new ProductAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            /*If there are no remaining events to execute*/
            if (events == null || !events.Any())
                return aggregate;
            /*Else*/
            aggregate.ReplayEvents(events);
            aggregate.Version = events.Select(x => x.Version).Max();
            return aggregate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommitedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommited();
        }
    }
}
