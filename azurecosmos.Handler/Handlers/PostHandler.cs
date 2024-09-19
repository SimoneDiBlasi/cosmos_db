using azurecosmos.Core.Interfaces;
using azurecosmos.Core.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

namespace azurecosmos.Handler.Handlers
{
    public class PostHandler(CosmosClient cosmosClient, IConfiguration configuration) : IPost
    {
        private readonly CosmosClient cosmosClient = cosmosClient;
        private readonly IConfiguration configuration = configuration;

        public async Task<List<Post>> GetAllPostsByPartitionKeyAsync(string partitionKey)
        {
            var container = cosmosClient.GetContainer(configuration["CosmosDB:DatabaseName"], "social-media-feed");
            var query = container.GetItemLinqQueryable<Post>(requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(partitionKey)
            })
            .Where(item => item.PartitionKey == partitionKey)
            .ToFeedIterator();


            var items = new List<Post>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                items.AddRange(response);
            }
            return items;
        }

        public async Task<Post> GetPostByIdAsync(string id, string partitionKey)
        {
            var container = cosmosClient.GetContainer(configuration["CosmosDB:DatabaseName"], "social-media-feed");
            var response = await container.ReadItemAsync<Post>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var container = cosmosClient.GetContainer(configuration["CosmosDB:DatabaseName"], "social-media-feed");
            var newPost = await container.CreateItemAsync(post, new PartitionKey(post.PartitionKey));
            return newPost.Resource;
        }

        public async Task<Post> DeletePostAsync(string id, string partitionKey)
        {
            var container = cosmosClient.GetContainer(configuration["CosmosDB:DatabaseName"], "social-media-feed");
            var deletedPost = await container.DeleteItemAsync<Post>(id, new PartitionKey(partitionKey));
            return deletedPost.Resource;
        }

        public async Task<Post> UpdatePostAsync(string partitionKey, Post post)
        {
            var container = cosmosClient.GetContainer(configuration["CosmosDB:DatabaseName"], "social-media-feed");
            var updatedPost = await container.UpsertItemAsync(post, new PartitionKey(partitionKey));
            return updatedPost.Resource;
        }
    }
}
