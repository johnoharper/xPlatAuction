using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace xPlatAuction
{
    public class AuctionService
    {
        private static MobileServiceClient azClient;

        public AuctionService(string serviceBaseUri)
        {
            //Use the application key here and in the web.config of the backend for cross machine access
            azClient = new MobileServiceClient(serviceBaseUri, "xPlatAuction");
            //azClient = new MobileServiceClient(serviceBaseUri);
        }

        public Uri ServiceBaseUri { 
            get { return azClient.ApplicationUri; }
        }

        public MobileServiceUser CurrentUser
        {
            get { return azClient.CurrentUser; }
            set { azClient.CurrentUser = value; }
        }

        public async Task<IEnumerable<Auction>>GetAuctions()
        {
            var table = azClient.GetTable<Auction>();
            return await table.ReadAsync();
        }

        public async Task<IEnumerable<AuctionItem>> GetItems(string auctionId)
        {
            var table = azClient.GetTable<AuctionItem>();
            var query = table.Where(ai=>ai.AuctionId == auctionId);
            return await table.ReadAsync(query);
        }

        public async Task<Bid> PlaceBid(Bid newBid)
        {
            var table = azClient.GetTable<Bid>();
            await table.InsertAsync(newBid);
            return newBid;
        }

        public async Task<IEnumerable<MyAuctionItem>> GetMyItems()
        {
            //NOTE: use non-generic overload to return data
            //without serialization
            return await azClient.InvokeApiAsync<IEnumerable<MyAuctionItem>>("MyItems", HttpMethod.Get, null);
        }
    }
}
