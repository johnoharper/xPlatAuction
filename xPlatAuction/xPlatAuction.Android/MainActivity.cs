using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace xPlatAuction.Droid
{

	[Activity(Label = "xPlatAuction")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			//Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();
            Xamarin.Forms.Forms.Init(this, bundle);

			var xApp = new xPlatAuction.App ();
			xApp.LoadMainPage();
			LoadApplication (xApp);

        }
    }
}

