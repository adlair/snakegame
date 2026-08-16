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

namespace SnakeGame.Activities
{
	[Activity (Label = "NormalActivity")]			
	public class NormalActivity : Activity
	{
		private NormalGameView gameView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature (WindowFeatures.NoTitle);
			SetContentView (Resource.Layout.Normal);
			gameView = FindViewById<NormalGameView> (Resource.Id.snake);

			if (bundle == null) {
				gameView.TextScore = FindViewById<TextView> (Resource.Id.txvScore);
			}
		}
	}
}

