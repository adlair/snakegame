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
	[Activity (Label = "GraphicActivity")]			
	public class GraphicActivity : Activity
	{
		private GraphicGameView gameView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature (WindowFeatures.NoTitle);
			SetContentView (Resource.Layout.Graphic);
			gameView = FindViewById<GraphicGameView> (Resource.Id.snake);

			if (bundle == null) {
				gameView.TextScore = FindViewById<TextView> (Resource.Id.txvScoreG);
			}
		}
	}
}