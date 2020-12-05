using AuctionConsoleApp.Models;
using System;
using System.Collections.Generic;

namespace AuctionConsoleApp
{
    public class AuctionProcessor
    {
        private readonly Config _config;

        public AuctionProcessor(Config config)
        {
            _config = config;
        }

        public IList<IList<Bid>> ProcessResult(IList<SiteBid> input)
        {
            throw new NotImplementedException();
        }
    }
}