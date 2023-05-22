using APIGateway.Aggregators;
using APIGateway.Configurations;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerForOcelot(builder.Configuration,
    (o) =>
        {
            o.GenerateDocsForAggregates = true;
        });

builder.Services.AddOcelot(builder.Configuration)
    .AddSingletonDefinedAggregator<ItemAggregator>()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Services.AddJwtAuthentication();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
}).UseOcelot().Wait();

app.UseAuthentication();
app.UseAuthorization();
app.Run();