using Catalog.Query.Infrastructure.Converters;
using Catalog.Query.Infrastructure.Handlers;
using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Catalog.Query.Infrastructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly ICatalogEventHandler _catalogEventHandler;

        public EventConsumer(IOptions<ConsumerConfig> config, ICatalogEventHandler catalogEventHandler)
        {
            _config = config.Value;
            _catalogEventHandler = catalogEventHandler;
        }

        public Task Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config).SetKeyDeserializer(Deserializers.Utf8).SetValueDeserializer(Deserializers.Utf8).Build();
            consumer.Subscribe(topic);
            while (true)
            {
                var consumerResult = consumer.Consume();
                if (consumerResult == null)
                    continue;
                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);
                var handlerMethod = _catalogEventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });
                if (handlerMethod == null)
                    throw new ArgumentNullException(nameof(handlerMethod), "Cound not find event handler method");
                handlerMethod.Invoke(_catalogEventHandler, new object[] { @event });
                consumer.Commit();
            }
        }
    }
}
