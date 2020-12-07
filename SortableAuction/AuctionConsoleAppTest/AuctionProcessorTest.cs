using AuctionConsoleApp;
using AuctionConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AuctionConsoleAppTest
{
    public class AuctionProcessorTest
    {
        private const string SITE_ONE_NAME = "site1";
        private const double SITE_ONE_FLOOR = 1;
        private const string BIDDER_ONE_NAME = "AUCT";
        private const double BIDDER_ONE_ADJUSTMENT = -0.5f;
        private const string UNIT_ONE_NAME = "unit1";
        private const double BID_ONE_PRICE = 10;
        private const double BID_TWO_PRICE = 20;
        private const string SITE_TWO_NAME = "site2";
        private const double SITE_TWO_FLOOR = 2;
        private const string BIDDER_TWO_NAME = "DUCT";
        private const double BIDDER_TWO_ADJUSTMENT = 0.5f;
        private const string UNIT_TWO_NAME = "unit2";
        private const double BID_THREE_PRICE = 30;
        private const string UNKNOWN_BIDDER = "UknownBidder";

        [Fact]
        public void TestRunAuction_WithOneSiteAndMultipleBids_ExpectedWinningBid()
        {
            var config = CreateConfigWithOneSite();
            var siteBids = CreateBidsForOneSite();

            var actual = new AuctionProcessor(config).RunAuction(siteBids);
            var expected = CreateResultForOneSite();

            AssertResultEqual(expected, actual);
        }

        [Fact]
        public void TestRunAuction_WithMultipleSitesAndBids_ExpectedWinningBids()
        {
            var config = CreateConfigWithMultipleSites();
            var siteBids = CreateBidsForMultipleSites();

            var actual = new AuctionProcessor(config).RunAuction(siteBids);
            var expected = CreateResultForMultipleSite();

            AssertResultEqual(expected, actual);
        }

        [Fact]
        public void TestRunAuction_WithInvalidSite_ExpectedEmptyList()
        {
            var config = CreateConfigWithOneSite();
            var siteBids = CreateBidsForOneSite();
            siteBids[0].SiteName = "INVALID_SITE_NAME";

            var actual = new AuctionProcessor(config).RunAuction(siteBids);

            AssertEmptyList(actual);
        }

        [Fact]
        public void TestRunAuction_WithInvalidBids_ExpectedEmptyList()
        {
            var config = CreateConfigWithOneSite();
            var siteBids = CreateInvalidBidsForOneSite();

            var actual = new AuctionProcessor(config).RunAuction(siteBids);

            AssertEmptyList(actual);
        }

        private void AssertEmptyList(IList<IList<Bid>> actual)
        {
            Assert.True(actual.Count == 1);
            Assert.True(actual[0].Count == 0);
        }

        private void AssertResultEqual(IList<IList<Bid>> expected, IList<IList<Bid>> actual)
        {
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(expected[i].Count, actual[i].Count);

                for (int j = 0; j < actual[i].Count; j++)
                {
                    Assert.Equal(expected[i][j].BidderName, actual[i][j].BidderName);
                    Assert.Equal(expected[i][j].Price, actual[i][j].Price);
                    Assert.Equal(expected[i][j].Unit, actual[i][j].Unit);
                }
            }
        }


        private IList<IList<Bid>> CreateResultForOneSite()
        {
            return new List<IList<Bid>>()
            {
                new List<Bid>()
                {
                   CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_TWO_PRICE)
                }
            };
        }

        private IList<IList<Bid>> CreateResultForMultipleSite()
        {
            return new List<IList<Bid>>()
            {
                new List<Bid>()
                {
                   CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_TWO_PRICE)
                },

                 new List<Bid>()
                {
                   CreateBid(BIDDER_TWO_NAME, UNIT_ONE_NAME, BID_TWO_PRICE),
                   CreateBid(BIDDER_TWO_NAME, UNIT_TWO_NAME, BID_THREE_PRICE)
                }
            };
        }

        private IList<SiteBid> CreateBidsForOneSite()
        {
            var units = new List<string>()
            {
                UNIT_ONE_NAME
            };

            var bids = new List<Bid>()
            {
                CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_TWO_PRICE),
                CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_ONE_PRICE)
            };

            return new List<SiteBid>
            {
                CreateSiteBid(SITE_ONE_NAME, units, bids)
            };
        }



        private IList<SiteBid> CreateBidsForMultipleSites()
        {
            var units = new List<string>()
            {
                UNIT_ONE_NAME,
                UNIT_TWO_NAME
            };

            var siteOneBids = new List<Bid>()
            {
                CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_ONE_PRICE),
                CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_TWO_PRICE)
            };

            var siteTwoBids = new List<Bid>()
            {
                CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, BID_ONE_PRICE),
                CreateBid(BIDDER_TWO_NAME, UNIT_TWO_NAME, BID_TWO_PRICE),
                CreateBid(BIDDER_TWO_NAME, UNIT_TWO_NAME, BID_THREE_PRICE),
                CreateBid(BIDDER_TWO_NAME, UNIT_ONE_NAME, BID_TWO_PRICE)
            };

            return new List<SiteBid>
            {
                CreateSiteBid(SITE_ONE_NAME, units, siteOneBids),
                 CreateSiteBid(SITE_TWO_NAME, units, siteTwoBids)
            };
        }

        private IList<SiteBid> CreateInvalidBidsForOneSite()
        {
            var units = new List<string>()
            {
                UNIT_ONE_NAME
            };

            var invalidBids = new List<Bid>()
            {
                // bidder invalid by it's name 
                CreateBid("InvalidBidder", UNIT_ONE_NAME, BID_ONE_PRICE),
                
                // bidder invalid by it's unit
                CreateBid(BIDDER_ONE_NAME, "InvalidUnit", BID_TWO_PRICE),

                // bidder invalid by being under the floor after adjustments
                CreateBid(BIDDER_ONE_NAME, UNIT_ONE_NAME, 1),
                
                // an unknown bidder that exists in the site's list of bidders but not in the main list of bidders
                CreateBid(UNKNOWN_BIDDER, UNIT_ONE_NAME, 1)
            };

            return new List<SiteBid>
            {
                CreateSiteBid(SITE_ONE_NAME, units, invalidBids),
            };
        }

        private Config CreateConfigWithOneSite()
        {
            var bidders = new List<Bidder>
            {
                CreateBidder(BIDDER_ONE_NAME, BIDDER_ONE_ADJUSTMENT),
                CreateBidder(UNKNOWN_BIDDER, BIDDER_ONE_ADJUSTMENT)
            };

            var sites = new List<Site>
            {
                CreateSite(SITE_ONE_NAME, bidders, SITE_ONE_FLOOR)
            };

            return new Config()
            {
                Sites = sites,
                Bidders = bidders
            };
        }

        private Config CreateConfigWithMultipleSites()
        {
            var bidders = new List<Bidder>
            {
                CreateBidder(BIDDER_ONE_NAME, BIDDER_ONE_ADJUSTMENT),
                CreateBidder(BIDDER_TWO_NAME, BIDDER_TWO_ADJUSTMENT)
            };

            var sites = new List<Site>
            {
                CreateSite(SITE_ONE_NAME, bidders, SITE_ONE_FLOOR),
                CreateSite(SITE_TWO_NAME, bidders, SITE_TWO_FLOOR)
            };

            return new Config()
            {
                Sites = sites,
                Bidders = bidders
            };
        }

        private Bid CreateBid(string name, string unitName, double price)
        {
            return new Bid
            {
                BidderName = name,
                Price = price,
                Unit = unitName
            };
        }

        private SiteBid CreateSiteBid(string name, List<string> units, List<Bid> bids)
        {
            return new SiteBid()
            {
                SiteName = name,
                Units = units,
                Bids = bids
            };
        }

        private Bidder CreateBidder(string name, double adjustment)
        {
            return new Bidder
            {
                Name = name,
                Adjustment = adjustment
            };
        }

        private Site CreateSite(string name, List<Bidder> bidders, double floor)
        {
            return new Site
            {
                Name = name,
                BiddersName = bidders.Select(x => x.Name).ToList(),
                Floor = floor
            };
        }
    }
}