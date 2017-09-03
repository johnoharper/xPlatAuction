
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using xPlatAuction;
using MonoTouch.FacebookConnect;


namespace xPlatAuction.iOS
{
    public partial class LoginViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public LoginViewController()
			: base()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();      
        }
			
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var auctionService = App.GetAuctionService();
			LocalFBLogin (auctionService);
        }

			
		private async void LocalFBLogin(AuctionService service)
		{
			if (FBSession.ActiveSession.State == FBSessionState.Open
				|| FBSession.ActiveSession.State == FBSessionState.CreatedTokenLoaded)
			{
				//already logged into FB session, use token to login with Azure Mobile
				await service.Login(FBSession.ActiveSession.AccessTokenData.AccessToken);

				FinishedLoggingIn ();
			}
			else
			{
				//not already logged in, so open FB session
				//this will cause login screen if needed.
				FBSession.OpenActiveSession(new string[] { "public_profile" }, true, async (fbs, fbss, err) =>
					{
						//now login with FB token
						await service.Login(fbs.AccessTokenData.AccessToken);

						FinishedLoggingIn();
					});
			}
		}

		private void FinishedLoggingIn()
		{
			xPlatAuction.App app = Xamarin.Forms.Application.Current as xPlatAuction.App;

			//remove login controller window
			UIWindow loginWindow = UIApplication.SharedApplication.Windows[1];
			loginWindow.Dispose();

			//change page in the Xamarin app to bring window forward
			app.LoadMainPage ();
			RegisterForNotifications ();
		}

		private async void RegisterForNotifications()
		{
			var notSettings = UIUserNotificationSettings.GetSettingsForTypes (
				UIUserNotificationType.Alert | UIUserNotificationType.Badge, new NSSet ());

			UIApplication.SharedApplication.RegisterUserNotificationSettings (notSettings);
			UIApplication.SharedApplication.RegisterForRemoteNotifications ();

		}
    }


}