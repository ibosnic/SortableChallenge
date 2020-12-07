using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuctionConsoleApp.Models
{
    public class Site
    {
        private HashSet<string> _bidderNames = new HashSet<string>();

        [JsonProperty("name")]
        public string Name 
        {
            get; set;
        }

        [JsonProperty("bidders")]
        public IList<string> BiddersName
        {
            set
            {
                AddAllBidderNames(value);
            }   
        }

        [JsonProperty("floor")]
        public float Floor
        {
            get; set;
        }

        private void AddAllBidderNames(IList<string> bidderNames)
        {
            _bidderNames = new HashSet<string>();
            
            foreach (var bidderName in bidderNames)
            {
                _bidderNames.Add(bidderName);
            }
        }

        public bool ContainsBidderName(string name)
        {
            return _bidderNames.Contains(name);
        }
    }
}