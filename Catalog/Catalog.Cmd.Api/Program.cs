using Catalog.Cmd.Api.CommandHandlers;
using Catalog.Cmd.Api.Commands;
using Catalog.Cmd.Domain.Aggregates;
using Catalog.Cmd.Infrastructure.Config;
using Catalog.Cmd.Infrastructure.Dispatcher;
using Catalog.Cmd.Infrastructure.Handler;
using Catalog.Cmd.Infrastructure.Producers;
using Catalog.Cmd.Infrastructure.Repository;
using Catalog.Cmd.Infrastructure.Store;
using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<ProductAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICatalogCommandHandler, CatalogCommandHandler>();

/*Register command methods*/
var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICatalogCommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<ProductCreateCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ProductDeleteCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ProductEditNameDescriptionCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ProductEditValueCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ProductEditStockCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ProductChangeCategoryCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ProductCategoryCreateCommand>(commandHandler.HandleAsync);

builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
