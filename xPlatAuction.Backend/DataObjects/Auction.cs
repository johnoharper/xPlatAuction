using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xPlatAuction.Backend.DataObjects
{
    public class Auction : EntityData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //EF Properties
        public virtual Collection<AuctionItemDBEntity> Items { get; set; }

    }
}
