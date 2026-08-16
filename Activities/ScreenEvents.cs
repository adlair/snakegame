using System;
using Android.OS;

namespace SnakeGame.Activities
{
	public class ScreenEvents : Handler
	{
		private GraphicGameView graphicView;
		private NormalGameView normalView;

		public ScreenEvents (GraphicGameView view)
		{
			this.graphicView = view;
		}

		public ScreenEvents(NormalGameView view)
		{
			this.normalView = view;
		}

		public override void HandleMessage (Message msg)
		{
			if (graphicView != null) {
				graphicView.BuildWorld ();
				graphicView.Invalidate ();
			} else {
				normalView.BuildWorld ();
				normalView.Invalidate ();
			}
		}

		public void Delay (long delayMove)
		{
			this.RemoveMessages (0);
			SendMessageDelayed (ObtainMessage (0), delayMove);
		}
	}
}