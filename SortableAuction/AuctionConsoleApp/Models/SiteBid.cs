using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AuctionConsoleApp.Models
{
    public class SiteBid
    {
        private HashSet<string> _units = new HashSet<string>();
        
        [JsonProperty("site")]
        public string SiteName
        {
            get; set;
        }

        [JsonProperty("units")]
        public IList<string> Units
        {
            set
            {
                AddAllUnits(value);
            }
        }

        [JsonProperty("bids")]
        public IList<Bid> Bids
        {
            get; set;
        }

        private void AddAllUnits(IList<string> units)
        {
            _units = new HashSet<string>();

            foreach (var unit in units)
            {
                _units.Add(unit);
            }
        }

        public bool ContainsUnit(string name)
        {
            return _units.Contains(name);
        }

    }
}
