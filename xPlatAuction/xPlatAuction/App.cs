using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace xPlatAuction
{

	public class App : Xamarin.Forms.Application
    {
	
        private static AuctionService azService;

        public App()
        {
			MainPage = new ContentPage ();
            //MainPage = new NavigationPage(
                //new Auctions());
        }

		public void LoadMainPage()
		{
			MainPage = new NavigationPage (
				new Auctions ());
		}
        public static AuctionService GetAuctionService()
        {
            if (azService == null)
            {
                azService = new AuctionService("https://<your mobile service name>.azure-mobile.net/");
            }
            return azService;
        }
    }
}
