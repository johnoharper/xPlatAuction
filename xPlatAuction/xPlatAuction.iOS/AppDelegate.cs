using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using MonoTouch.FacebookConnect;
using Xamarin.Forms.Platform.iOS;

namespace xPlatAuction.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
		UIWindow window;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			//show alerts if launched from notification
			ShowAlerts (options);

            FBSettings.DefaultAppID = "1539933046264301";
            FBSettings.DefaultDisplayName = "xPlatAuction";

            if (FBSession.ActiveSession.State == FBSessionState.CreatedTokenLoaded)
            {
                FBSession.OpenActiveSession(
                    new string[] { "public_profile" },
                    false,
                    (fbs, fbss, e) =>
                    {
                        Console.Out.Write(fbss);
                    });
            } 

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();
            Forms.Init();
			LoadApplication (new xPlatAuction.App ());

            //call this BEFORE making login window key
			bool baseHasFinished = base.FinishedLaunching(app, options);

			//create login window and make key
			window = new UIWindow(UIScreen.MainScreen.Bounds);

			window.RootViewController = new LoginViewController();
			window.MakeKeyAndVisible ();

			return baseHasFinished;

        }

        private void FBStateChanged (FBSession session, FBSessionState state, Exception error)
        {}
        public override void OnActivated(UIApplication application)
        {
            FBSession.ActiveSession.HandleDidBecomeActive();
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to FBSession in order for SSO authentication
            // to work.
            return FBSession.ActiveSession.HandleOpenURL(url);
        }

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			var xPlatService = xPlatAuction.App.GetAuctionService ();
			xPlatService.RegisterForPushNotifications (
				deviceToken);
			//NotificationRegistrationState.SetDeviceToken (deviceToken);
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			ShowAlerts (userInfo);
		}

		private void ShowAlerts(NSDictionary options)
		{
			if (options != null && options.ContainsKey(new NSString("aps"))) {
				NSDictionary apsInfo = (NSDictionary)options.ObjectForKey (new NSString("aps"));
				NSString alertTitle = (NSString)apsInfo.ObjectForKey (new NSString("alert"));
				UIAlertView alertView = new UIAlertView (
					                       alertTitle, alertTitle, null, "Ok", null);
				alertView.Show ();
			}
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{

		}
		public override void DidRegisterUserNotificationSettings (UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			var types = notificationSettings.Types;
			Console.WriteLine ("notifications");
		}
    }
}
