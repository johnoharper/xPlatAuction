using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using Newtonsoft.Json.Linq;
using MonoTouch.Foundation;

namespace xPlatAuction.iOS
{
    public static class AuctionServiceExtensions
    {

        public static async Task Login(this AuctionService service, UIViewController controller)
        {
            var auctionService = App.GetAuctionService();
            var client = new MobileServiceClient(auctionService.ServiceBaseUri);
            var user = await client.LoginAsync(controller, 
				MobileServiceAuthenticationProvider.Facebook);

            auctionService.CurrentUser = user;
        }

        public static async Task Login(this AuctionService service, string fbAccessToken)
        {
            JObject tokenPayload = new JObject();
            tokenPayload.Add("access_token", fbAccessToken);
            var auctionService = App.GetAuctionService();
            var client = new MobileServiceClient(auctionService.ServiceBaseUri);
            var user = await client.LoginAsync(
				MobileServiceAuthenticationProvider.Facebook,
				tokenPayload );

            auctionService.CurrentUser = user;
        }

		private static string NOTIFICATION_TEMPLATE = "{\"aps\": {\"alert\":\"{$(ItemName) + \' is now at \' + $(BidAmount)}\"}}";

		public static async Task RegisterForPushNotifications(this AuctionService service, NSData token)
		{
			var auctionService = App.GetAuctionService();
			var client = new MobileServiceClient(auctionService.ServiceBaseUri);
			client.CurrentUser = auctionService.CurrentUser;

			await client.GetPush ().RegisterTemplateAsync (token,
				NOTIFICATION_TEMPLATE, "", "BidTemplate");
		}
    }
}
