using Catalog.Query.Domain.Repositories;
using Catalog.Query.Infrastructure.Consumers;
using Catalog.Query.Infrastructure.DataAccess;
using Catalog.Query.Infrastructure.Handlers;
using Catalog.Query.Infrastructure.Repositories;
using Confluent.Kafka;
using CQRS.Core.Consumers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Action<DbContextOptionsBuilder> configureDbContext = x =>
{
    x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
};

builder.Services.AddDbContext<DataBaseContext>(configureDbContext);
builder.Services.AddSingleton(new DataBaseContextFactory(configureDbContext));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<ICatalogEventHandler, Catalog.Query.Infrastructure.Handlers.CatalogEventHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddHostedService<ConsumerHostedService>();

/*Create database from code*/
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DataBaseContext>();
dataContext.Database.EnsureCreated();

// Add services to the container.
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
