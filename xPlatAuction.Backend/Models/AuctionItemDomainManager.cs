using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xPlatAuction.Backend.DataObjects;
using xPlatAuction.Backend.Entities;

namespace xPlatAuction.Backend.Models
{
    public class AuctionItemDomainManager : MappedEntityDomainManager<AuctionItem, AuctionItemDBEntity>
    {
        public AuctionItemDomainManager(DbContext context, 
            System.Net.Http.HttpRequestMessage request, ApiServices services): base(context, request, services)
        { }

        public override IQueryable<AuctionItem> Query()
        {
            MobileServiceContext ctx = this.Context as MobileServiceContext;

            var items = from ai in ctx.AuctionItems
                select new AuctionItem
                {
                    Id = ai.Id,
                    Description = ai.Description,
                    StartingBid = ai.StartingBid,
                    Name = ai.Name,
                    AuctionId = ai.AuctionId,
                    CurrentBid = ai.Bids.Count == 0 ? 0 : ai.Bids.Max(b=>b.BidAmount)
                };

            return items;
        }
       
        public override System.Web.Http.SingleResult<AuctionItem> Lookup(string id)
        {
            var ctx = Context as MobileServiceContext;
            var item = from ai in ctx.AuctionItems
                       where ai.Id == id
                       select new AuctionItem
                       {
                           Id = ai.Id,
                           Description = ai.Description,
                           StartingBid = ai.StartingBid,
                           Name = ai.Name,
                           AuctionId = ai.AuctionId,
                           CurrentBid = ai.Bids.Count == 0 ? 0 : ai.Bids.Max(b => b.BidAmount)
                       };

            return new System.Web.Http.SingleResult<AuctionItem>(item);
        }

        public override Task<bool> DeleteAsync(string id)
        {
            return base.DeleteItemAsync(id);
        }
        public override Task<AuctionItem> UpdateAsync(string id, System.Web.Http.OData.Delta<AuctionItem> patch)
        {
            return base.UpdateEntityAsync(patch, id);
        }

       
    }
}
