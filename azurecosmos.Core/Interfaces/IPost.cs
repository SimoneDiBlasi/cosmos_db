using azurecosmos.Core.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azurecosmos.Core.Interfaces
{
    public interface IPost
    {
        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>List of posts.</returns>
        Task<List<Post>> GetAllPostsByPartitionKeyAsync(string partitionKey);

        /// <summary>
        /// Retrieves a specific post by its ID.
        /// </summary>
        /// <param name="id">Post ID.</param>
        /// <returns>A post object.</returns>
        Task<Post> GetPostByIdAsync(string id, string partitionKey);

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="post">Post object.</param>
        /// <returns>The created post.</returns>
        Task<Post> CreatePostAsync(Post post);

        /// <summary>
        /// Updates an existing post by ID.
        /// </summary>
        /// <param name="id">Post ID.</param>
        /// <param name="post">Updated post object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<Post> UpdatePostAsync(string partitionKey, Post post);

        /// <summary>
        /// Deletes a post by ID.
        /// </summary>
        /// <param name="id">Post ID.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<Post> DeletePostAsync(string id, string partitionKey);
    }
}

