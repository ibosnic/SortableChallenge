
using Newtonsoft.Json;

namespace AuctionConsoleApp.Models
{
    public class Bid
    {
        [JsonProperty("bidder")]
        public string BidderName
        {
            get; set;
        }

        [JsonProperty("unit")]
        public string Unit 
        {
            get; set;
        }

        [JsonProperty("bid")]
        public float Price
        {
            get; set;
        }
    }
}
