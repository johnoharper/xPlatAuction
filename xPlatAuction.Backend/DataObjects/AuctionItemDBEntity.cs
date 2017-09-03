using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xPlatAuction.Backend.Entities;

namespace xPlatAuction.Backend.DataObjects
{
    public class AuctionItemDBEntity : EntityData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double StartingBid { get; set; }


        //EF properties
        [Column("Auction_Id")]
        public string AuctionId { get; set; }

        [ForeignKey("AuctionId")]
        public virtual Auction Auction { get; set; }

        public virtual Collection<Bid> Bids { get; set; }
    }
}
