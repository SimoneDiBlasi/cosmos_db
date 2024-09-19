using azurecosmos.Core.Interfaces;
using azurecosmos.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace azurecosmos.Controllers
{
    [ApiController]
    [Route("api")]
    public class CosmosDBController(IPost post) : ControllerBase
    {
        public readonly IPost _post = post;

        [HttpGet("{partitionKey}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(string partitionKey)
        {
            try
            {
                var posts = await post.GetAllPostsByPartitionKeyAsync(partitionKey);
                return Ok(posts);
            }
            catch (CosmosException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{partitionKey}/{id}")]
        public async Task<ActionResult<Post>> GetPost(string partitionKey, string id)
        {
            try
            {
                var post = await _post.GetPostByIdAsync(id, partitionKey);
                return Ok(post);
            }
            catch (CosmosException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
        {
            try
            {
                var newPost = await _post.CreatePostAsync(post);
                return Ok(newPost);
            }
            catch (CosmosException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{partitionKey}/{id}")]
        public async Task<IActionResult> UpdatePost(string partitionKey, [FromBody] Post post)
        {
            try
            {
                var updatedPost = await _post.UpdatePostAsync(partitionKey, post);
                return Ok(updatedPost);
            }
            catch (CosmosException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{partitionKey}/{id}")]
        public async Task<IActionResult> DeletePost(string partitionKey, string id)
        {
            try
            {
                var posts = await post.DeletePostAsync(id, partitionKey);
                return Ok(posts);
            }
            catch (CosmosException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
