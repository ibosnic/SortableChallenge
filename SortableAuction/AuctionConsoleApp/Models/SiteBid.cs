using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuctionConsoleApp.Models
{
    public class SiteBid
    {
        [JsonProperty("site")]
        public string SiteName
        {
            get; set;
        }

        [JsonProperty("units")]
        public IList<string> Units
        {
            get; set;
        }

        [JsonProperty("bids")]
        public IList<Bid> Bids
        {
            get; set;
        }
    }
}
