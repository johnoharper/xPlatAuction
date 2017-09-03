using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace xPlatAuction.Droid
{
   public static class AuctionServiceExtensions
    {
       /// <summary>
       /// Logs in the user with a popup dialog for the google auth UI
       /// </summary>
       /// <param name="service">The object to extend</param>
       /// <param name="ctx">The UI context to use for rooting the login dialog.</param>
       public static async Task Login(this AuctionService service, Context ctx)
       {
           var auctionService = App.GetAuctionService();
           var client = new MobileServiceClient(auctionService.ServiceBaseUri);
           var user = await client.LoginAsync(ctx, MobileServiceAuthenticationProvider.Google);

           auctionService.CurrentUser = user;
       }

       /// <summary>
       /// Logs the user in with the authentication token provided for Google
       /// </summary>
       /// <param name="service">The object to extend</param>
       /// <param name="authToken">The token acqiured from Google APIs</param>
       public static async Task Login(this AuctionService service, string authToken)
       {
           JObject tokenObject = CreateTokenObject(authToken);
           var auctionService = App.GetAuctionService();
           var client = new MobileServiceClient(auctionService.ServiceBaseUri);
           var user = await client.LoginAsync( MobileServiceAuthenticationProvider.Google,tokenObject);

           auctionService.CurrentUser = user;
       }

       private static JObject CreateTokenObject(string authToken)
       {
           JObject tokenObject = new JObject();
           tokenObject.Add("id_token", authToken);
           return tokenObject;
       }

       private static string AlertTemplate = 
			"{\"data\":{\"item\":\"$(ItemName)\", \"newAmount\":\"$(BidAmount)\"}}";
       
		public static Task RegisterForNotifications(this AuctionService service, 
			string deviceUri)
       {
           MobileServiceClient client =
               new MobileServiceClient(service.ServiceBaseUri);
           client.CurrentUser = service.CurrentUser;
           return client.GetPush().RegisterTemplateAsync(
				deviceUri, AlertTemplate, "BidNotification");
       }

    }
}