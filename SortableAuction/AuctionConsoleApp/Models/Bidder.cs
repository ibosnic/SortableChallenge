
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
        public double Adjustment
        {
            get; set;
        }
    }
}
