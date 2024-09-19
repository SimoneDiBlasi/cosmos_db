using Newtonsoft.Json;

namespace azurecosmos.Core.Models
{
    public class Post
    {
        /// <summary>
        /// Unique identifier for the post.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The ID of the user who created the post (also used as the partition key).
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Partition key for Cosmos DB (usually the userId in this case).
        /// </summary>
        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        /// <summary>
        /// The content of the post.
        /// </summary>
        [JsonProperty("content")]
        public Content Content { get; set; }

        /// <summary>
        /// List of hashtags/tags related to the post.
        /// </summary>
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Location information where the post was made.
        /// </summary>
        [JsonProperty("location")]
        public Location Location { get; set; }

        /// <summary>
        /// Number of likes on the post.
        /// </summary>
        [JsonProperty("likes")]
        public int Likes { get; set; }

        /// <summary>
        /// Timestamp of when the post was created.
        /// </summary>
        [JsonProperty("postedAt")]
        public DateTime PostedAt { get; set; }
    }

    public class Content
    {
        /// <summary>
        /// The text content of the post.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Optional image URL for the post.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }

    public class Location
    {
        /// <summary>
        /// City where the post was created.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Country where the post was created.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class Comment
    {
        /// <summary>
        /// ID of the user who made the comment.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Text content of the comment.
        /// </summary>
        [JsonProperty("comment")]
        public string CommentText { get; set; }

        /// <summary>
        /// Timestamp when the comment was made.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }

}
