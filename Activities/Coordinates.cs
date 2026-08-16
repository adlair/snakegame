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
	public class Coordinates
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Coordinates (int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}