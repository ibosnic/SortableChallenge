using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuctionConsoleApp.Models
{
    public class Config
    {
        [JsonProperty("sites")]
        public IList<Site> Sites
        {
            get;
            set;
        }

        [JsonProperty("bidders")]
        public IList<Bidder> Bidders 
        {
            get;
            set;
        }
    }
}
