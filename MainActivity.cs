using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SnakeGame.Activities
{
	[Activity (Label = "SnakeGame", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature (WindowFeatures.NoTitle);
			SetContentView (Resource.Layout.Main);

			Button btnNormal = FindViewById<Button> (Resource.Id.btnNormal);
			btnNormal.Click += (sender, e) => {
				StartActivity (typeof(NormalActivity));
			};

			Button btnGraphic = FindViewById<Button> (Resource.Id.btnGraphic);
			btnGraphic.Click += (sender, e) => {
				StartActivity (typeof(GraphicActivity));
			};

			Button btnAbout = FindViewById<Button> (Resource.Id.btnAbout);			
			btnAbout.Click += (sender, e) => {
				StartActivity (typeof(AboutActivity));
			};
		}
	}
}