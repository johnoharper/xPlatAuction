using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xPlatAuction
{
    public class MyAuctionItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CurrentBid { get; set; }
        public double MyHighestBid { get; set; }
    }
}
