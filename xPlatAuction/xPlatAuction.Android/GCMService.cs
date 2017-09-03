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
using Android.Support.V4.App;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
//[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
[assembly: UsesPermission(Name = "android.permission.RECEIVE_BOOT_COMPLETED")]

namespace xPlatAuction.Droid
{
	[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new[]{Android.Content.Intent.ActionBootCompleted})]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
	public class GCMBroadcastReceiver : GcmBroadcastReceiverBase<GCMService>
    {}

    [Service]
    public class GCMService : GcmServiceBase
    {

		public GCMService () :base(Constants.GCMProjectIdentifier)
		{

		}

		protected override void OnMessage(Context context, Intent intent)
        {
			string item = intent.GetStringExtra ("item");
			string rawAmount = intent.GetStringExtra ("newAmount");
			float amount = float.Parse (rawAmount);

						NotificationCompat.Builder messageBuilder = 
							new NotificationCompat.Builder (context)
								.SetContentTitle ("Bid notification")
								.SetContentText (String.Format ("{0} is now at {1}", 
								item, amount))
								.SetPriority (0)
								.SetSmallIcon (Resource.Drawable.Icon);
						NotificationManager notificationMgr = 
							(NotificationManager)context.GetSystemService (
								Context.NotificationService);
			
						notificationMgr.Notify (0, messageBuilder.Build ());
        }

        protected override async void OnRegistered(Context context, string registrationId)
        {
			var xPlatService = xPlatAuction.App.GetAuctionService ();
			await xPlatService.RegisterForNotifications (registrationId);
        }

		protected override void OnError (Context context, string errorId)
		{
			Console.Write (errorId);
		}
		protected override void OnUnRegistered (Context context, string registrationId)
		{

		}
    }
}