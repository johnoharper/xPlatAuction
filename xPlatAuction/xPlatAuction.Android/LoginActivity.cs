using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Android.Content;
using Gcm.Client;


namespace xPlatAuction.Droid
{
	[Activity (Label = "Login", MainLauncher=true, NoHistory=true)]			
	public class LoginActivity : Activity
	{
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();
			// Create your application here
			var service = App.GetAuctionService();
			await service.Login(this);

			RegisterForNotifications ();

			//after login succeeds, move on to the main activity
			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);
		}

		private async void RegisterForNotifications()
		{
			GcmClient.CheckDevice (this.ApplicationContext);
			GcmClient.CheckManifest (this.ApplicationContext);

			GcmClient.Register (this.ApplicationContext,Constants.GCMProjectIdentifier);

		}
	}
}

