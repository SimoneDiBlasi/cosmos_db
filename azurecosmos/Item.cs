using Newtonsoft.Json;

namespace item
{

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenandId")]  // Corrected the typo from 'TenandId' to 'TenantId'
        public string TenandId { get; set; }  // Partition key

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }


}
