using azurecosmos.Core.Interfaces;
using azurecosmos.Handler.Handlers;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CosmosClient>(cosmosClient => new CosmosClient(_configuration["CosmosDB:ConnectionString"]));
builder.Services.AddScoped<IPost, PostHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
