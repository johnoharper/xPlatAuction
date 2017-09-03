using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xPlatAuction.Backend.DataObjects;

namespace xPlatAuction.Backend.DataObjects
{
    public class Bid : EntityData
    {
        public double BidAmount { get; set; }

        public string Bidder { get; set; }


        //EF properties
        [Column("AuctionItem_Id")]
        public string AuctionItemId  { get; set; }

        [ForeignKey("AuctionItemId")]
        public virtual AuctionItemDBEntity AuctionItem { get; set; }


    }
}
