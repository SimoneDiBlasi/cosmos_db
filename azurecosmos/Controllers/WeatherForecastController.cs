using Azure.Core.Pipeline;
using item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.ComponentModel;

namespace azurecosmos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, CosmosClient cosmosClient, IConfiguration configuration) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger = logger;
        private readonly CosmosClient _cosmosClient = cosmosClient;
        private readonly IConfiguration _configuration = configuration;
        [HttpGet("{tenantId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems(string tenantId)
        {
            var container = _cosmosClient.GetContainer(_configuration["CosmosDb:DatabaseName"], "test-container-id");
            var query = container.GetItemLinqQueryable<Item>(requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(tenantId)
            })
            .Where(item => item.TenandId == tenantId)
            .ToFeedIterator();

            
            var items = new List<Item>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                items.AddRange(response);
            }

            return Ok(query);
        }

        // GET: api/items/{tenantId}/{id}
        [HttpGet("{tenantId}/{id}")]
        public async Task<ActionResult<Item>> GetItem(string tenantId, string id)
        {
            var container = _cosmosClient.GetContainer(_configuration["CosmosDb:DatabaseName"], "test-container-id");
            try
            {
                var response = await container.ReadItemAsync<Item>(id, new PartitionKey(tenantId));
                return Ok(response.Resource);
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem([FromBody] Item item)
        {
            var container = _cosmosClient.GetContainer(_configuration["CosmosDb:DatabaseName"], "test-container-id");
            await container.CreateItemAsync(item, new PartitionKey(item.TenandId));
            return CreatedAtAction(nameof(GetItem), new { tenantId = item.TenandId, id = item.Id }, item);
        }

        // PUT: api/items/{tenantId}/{id}
        [HttpPut("{tenantId}/{id}")]
        public async Task<IActionResult> UpdateItem(string tenantId, string id, [FromBody] Item item)
        {
            var container = _cosmosClient.GetContainer(_configuration["CosmosDb:DatabaseName"], "test-container-id");
            if (id != item.Id || tenantId != item.TenandId)
            {
                return BadRequest();
            }

            try
            {
                await container.UpsertItemAsync(item, new PartitionKey(tenantId));
                return NoContent();
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        }

        // DELETE: api/items/{tenantId}/{id}
        [HttpDelete("{tenantId}/{id}")]
        public async Task<IActionResult> DeleteItem(string tenantId, string id)
        {
            var container = _cosmosClient.GetContainer(_configuration["CosmosDb:DatabaseName"], "test-container-id");
            try
            {
                await container.DeleteItemAsync<Item>(id, new PartitionKey(tenantId));
                return NoContent();
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        }
    }
}
