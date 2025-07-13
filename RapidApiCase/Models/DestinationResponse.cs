using Newtonsoft.Json;

namespace RapidApiCase.Models
{
    public class DestinationResponse
    {


        [JsonProperty("data")]
        public List<DestinationItem> Data { get; set; }


        public class DestinationItem
        {
            [JsonProperty("dest_id")]
            public string DestId { get; set; }

            [JsonProperty("dest_type")]
            public string DestType { get; set; }

            [JsonProperty("search_type")]
            public string SearchType { get; set; }

        }

    }
}
