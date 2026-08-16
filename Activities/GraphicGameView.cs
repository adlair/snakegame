using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SnakeGame.Activities
{
	public class GraphicGameView : Helper
	{
		#region Variables and Properties

		private int delayMove;
		private ScreenEvents screenEvents;
		private TextView textScore;
		private long Score = 0;
		private bool playing = false;
		private Direction direction = Direction.North;
		private static Random random = new Random ();
		private List<Coordinates> snake = new List<Coordinates> ();
		private List<Coordinates> apples = new List<Coordinates> ();

		public TextView TextScore {
			set{ textScore = value; }
		}

		#endregion

		#region Constructors

		public GraphicGameView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			GraphicMode = true;
			InitGameView ();
			screenEvents = new ScreenEvents (this);
		}

		public GraphicGameView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			GraphicMode = true;
			InitGameView ();
			screenEvents = new ScreenEvents (this);
		}

		#endregion

		#region Methods

		private void InitGameView ()
		{
			Focusable = true;
			FocusableInTouchMode = true;
			ResetTiles (9);
			LoadObject (GameObjects.Apple, Resources.GetDrawable (Resource.Drawable.Apple));
			LoadObject (GameObjects.BodyH, Resources.GetDrawable (Resource.Drawable.BodyH));
			LoadObject (GameObjects.BodyV, Resources.GetDrawable (Resource.Drawable.BodyV));
			LoadObject (GameObjects.HeadDown, Resources.GetDrawable (Resource.Drawable.HeadDown));
			LoadObject (GameObjects.HeadLeft, Resources.GetDrawable (Resource.Drawable.HeadLeft));
			LoadObject (GameObjects.HeadRight, Resources.GetDrawable (Resource.Drawable.HeadRight));
			LoadObject (GameObjects.HeadUP, Resources.GetDrawable (Resource.Drawable.HeadUP));
			LoadObject (GameObjects.Wall, Resources.GetDrawable (Resource.Drawable.Wall));
			Click += new EventHandler (GameView_onClick);
		}

		private void InitNewGame ()
		{
			snake.Clear ();
			apples.Clear ();
			snake.Add (new Coordinates (7, 7));
			snake.Add (new Coordinates (6, 7));
			snake.Add (new Coordinates (5, 7));
			snake.Add (new Coordinates (4, 7));
			snake.Add (new Coordinates (3, 7));
			snake.Add (new Coordinates (2, 7));
			AddApple ();
			AddApple ();
			AddApple ();
			AddApple ();
			AddApple ();
			delayMove = 600;
			textScore.Text = "Score: 0";
		}

		private void AddApple ()
		{
			Coordinates coordinates = null;
			bool invalidCoordinate = true;
			while (invalidCoordinate) {
				int randomX = 1 + random.Next (xMax - 2);
				int randomY = 1 + random.Next (yMax - 2);
				coordinates = new Coordinates (randomX, randomY);

				foreach (Coordinates _snake in snake) {
					if (_snake.X.Equals (coordinates.X) && _snake.Y.Equals (coordinates.Y))
						invalidCoordinate = true;
				}
				invalidCoordinate = false;
			}
			apples.Add (coordinates);
		}

		public void BuildWorld ()
		{
			if (playing) {
				ClearObjects ();
				BuildWalls ();
				BuildSnake ();
				BuildApples ();
			} else
				textScore.Text = "Game Over... Touch for play again!";
			screenEvents.Delay (delayMove);
		}

		private void BuildWalls ()
		{
			for (int x = 0; x < xMax; x++) {
				SetObject (GameObjects.Wall, x, 0);
				SetObject (GameObjects.Wall, x, yMax - 1);
			}

			for (int y = 1; y < yMax - 1; y++) {
				SetObject (GameObjects.Wall, 0, y);
				SetObject (GameObjects.Wall, xMax - 1, y);
			}
		}

		private void BuildApples ()
		{
			foreach (Coordinates c in apples)
				SetObject (GameObjects.Apple, c.X, c.Y);
		}

		private void BuildSnake ()
		{
			bool isGrowing = false;

			Coordinates head = snake [0];
			Coordinates newHead = new Coordinates (1, 1);

			switch (direction) {
			case Direction.East:
				newHead = new Coordinates (head.X + 1, head.Y);
				break;
			case Direction.West:
				newHead = new Coordinates (head.X - 1, head.Y);
				break;
			case Direction.North:
				newHead = new Coordinates (head.X, head.Y - 1);
				break;
			case Direction.South:
				newHead = new Coordinates (head.X, head.Y + 1);
				break;
			}
			if ((newHead.X < 1) || (newHead.Y < 1) || (newHead.X > xMax - 2)
			    || (newHead.Y > yMax - 2)) {
				playing = false;			 
				return;
			}

			foreach (Coordinates _snake in snake) {
				if (_snake.X.Equals (newHead.X) && _snake.Y.Equals (newHead.Y)) {
					playing = false;
					return;
				}
			}

			foreach (Coordinates apple in apples) {
				if (apple.X.Equals (newHead.X) && apple.Y.Equals (newHead.Y)) {
					apples.Remove (apple);
					AddApple ();
					Score++;
					textScore.Text = string.Format ("Score: {0} ", Score.ToString ());
					delayMove -= 20;
					isGrowing = true;
					break;
				}
			}

			snake.Insert (0, newHead);

			if (!isGrowing)
				snake.RemoveAt (snake.Count - 1);

			int index = 0; 

			foreach (Coordinates c in snake) {
				if (index == 0) {
					switch (direction) {
					case Direction.East:
						SetObject (GameObjects.HeadRight, c.X, c.Y);
						break;
					case Direction.West:
						SetObject (GameObjects.HeadLeft, c.X, c.Y);
						break;
					case Direction.North:
						SetObject (GameObjects.HeadUP, c.X, c.Y);
						break;
					case Direction.South:
						SetObject (GameObjects.HeadDown, c.X, c.Y);
						break;
					}					
				} else if (direction == Direction.East || direction == Direction.West)
					SetObject (GameObjects.BodyH, c.X, c.Y);
				else
					SetObject (GameObjects.BodyV, c.X, c.Y);
				index++;
			} 
		}

		#endregion

		#region Events

		public override bool OnKeyDown (Keycode keyCode, KeyEvent e)
		{
			switch (keyCode) {
			case Keycode.DpadUp:
				if (direction != Direction.South)
					direction = Direction.North;
				break;
			case Keycode.DpadDown:
				if (direction != Direction.North)
					direction = Direction.South;
				break;
			case Keycode.DpadLeft:
				if (direction != Direction.East)
					direction = Direction.West;
				break;
			case Keycode.DpadRight:
				if (direction != Direction.West)
					direction = Direction.East;
				break;
			}
			return base.OnKeyDown (keyCode, e);
		}

		private void GameView_onClick (object sender, EventArgs e)
		{
			if (!playing) {
				InitNewGame ();
				playing = true;
				BuildWorld ();
			}
		}

		#endregion
	}
}