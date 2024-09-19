using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var _configuration = builder.Configuration;


//Esiste un costruttore anche dove inserire una connection string
builder.Services.AddSingleton<CosmosClient>(cosmosClient =>
{
    var uri = _configuration["CosmosDb:URI"];
    var key = _configuration["CosmosDb:Key"];
    return new CosmosClient(uri, key);
});




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
