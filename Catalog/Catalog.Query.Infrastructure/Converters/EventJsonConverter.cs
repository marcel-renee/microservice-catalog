using Catalog.Common.Events;
using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalog.Query.Infrastructure.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }
        public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
            if (!doc.RootElement.TryGetProperty("Type", out var type))
                throw new JsonException("Could not detect the Type discriminator property!");
            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();
            return typeDiscriminator switch
            {
                nameof(ProductCreateEvent) => JsonSerializer.Deserialize<ProductCreateEvent>(json, options),
                nameof(ProductDeleteEvent) => JsonSerializer.Deserialize<ProductDeleteEvent>(json, options),
                nameof(ProductEditEvent) => JsonSerializer.Deserialize<ProductEditEvent>(json, options),
                nameof(ProductEditValueEvent) => JsonSerializer.Deserialize<ProductEditValueEvent>(json, options),
                nameof(ProductEditStockEvent) => JsonSerializer.Deserialize<ProductEditStockEvent>(json, options),
                nameof(ProductCategoryCreateEvent) => JsonSerializer.Deserialize<ProductCategoryCreateEvent>(json, options),
                _ => throw new JsonException($"Type discriminator {typeDiscriminator} is not supported yet!")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
