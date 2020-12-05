using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuctionConsoleApp.Models
{
    public class Site
    {
        [JsonProperty("name")]
        public string Name 
        {
            get; set;
        }

        [JsonProperty("bidders")]
        public IList<string> BiddersName
        {
            get;
            set;
        }

        [JsonProperty("floor")]
        public float Floor
        {
            get; set;
        }
    }
}