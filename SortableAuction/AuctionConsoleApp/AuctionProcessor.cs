using AuctionConsoleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuctionConsoleApp
{
    public class AuctionProcessor
    {
        private readonly Dictionary<string, Site> _sites;
        private readonly Dictionary<string, double> _bidders;

        public AuctionProcessor(Config config)
        {
            _sites = config.Sites != null ? config.Sites.ToDictionary(x => x.Name, x => x) : new Dictionary<string, Site>();
            _bidders = config.Bidders != null ? config.Bidders.ToDictionary(x => x.Name, x => x.Adjustment) : new Dictionary<string, double>();
        }

        /// <summary>
        /// Process the auction for each site returning a list of winning bids for each site
        /// </summary>
        /// <param name="siteBids">the list of sites and there bids</param>
        public IList<IList<Bid>> RunAuction(IList<SiteBid> siteBids)
        {
            var results = new List<IList<Bid>>();

            foreach (var currentSiteBid in siteBids)
            {
                if (!_sites.ContainsKey(currentSiteBid.SiteName))
                {
                    results.Add(new List<Bid>());
                    continue;
                }

                results.Add(ProcessBids(currentSiteBid, currentSiteBid.Bids));
            }

            return results;
        }

        private IList<Bid> ProcessBids(SiteBid currentSiteBid, IList<Bid> bids)
        {
            var largestBids = new Dictionary<string, Bid>();

            foreach (var bid in bids)
            {
                if(IsInValidBid(currentSiteBid, bid))
                {
                    continue;
                }

                var bidderAdjustment = _bidders[bid.BidderName];
                var currentBidderPrice = bid.Price + (bid.Price * bidderAdjustment);
                
                if(IsLowerThanFloor(currentSiteBid, currentBidderPrice))
                {
                    continue;
                }

                if (largestBids.ContainsKey(bid.Unit) && IsLargerThanLargestBid(currentBidderPrice, bidderAdjustment, largestBids[bid.Unit]))
                {
                    largestBids[bid.Unit] = bid;
                }
                
                if(!largestBids.ContainsKey(bid.Unit))
                {
                    largestBids.Add(bid.Unit, bid);
                }
            }

            return largestBids.Select(x => x.Value).ToList();
        }

        private bool IsLargerThanLargestBid(double currentBidderPrice, double bidderAdjustment, Bid largestBid)
        {
            var currentLargestBidderPrice = largestBid.Price + (largestBid.Price * bidderAdjustment);
            return currentBidderPrice > currentLargestBidderPrice;
        }

        private bool IsLowerThanFloor(SiteBid currentSiteBid, double currentBidderPrice)
        {
            return currentBidderPrice < _sites[currentSiteBid.SiteName].Floor;
        }

        private bool IsInValidBid(SiteBid currentSiteBid, Bid bid)
        {
            var site = _sites[currentSiteBid.SiteName];

            if(!site.ContainsBidderName(bid.BidderName) || !_bidders.ContainsKey(bid.BidderName))
            {
                return true;
            }

            if (!currentSiteBid.ContainsUnit(bid.Unit))
            {
                return true;
            }

            return false;
        }
    }
}