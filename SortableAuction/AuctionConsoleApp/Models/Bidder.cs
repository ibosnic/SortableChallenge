
using Newtonsoft.Json;

namespace AuctionConsoleApp.Models
{
    public class Bidder
    {
        [JsonProperty("name")]
        public string Name
        {
            get; set;
        }

        [JsonProperty("adjustment")]
        public float Adjustment
        {
            get; set;
        }
    }
}
