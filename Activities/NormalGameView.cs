using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace SnakeGame.Activities
{
	public class NormalGameView : Helper
	{
		#region Variables and Properties

		private ScreenEvents screenEvents;
		private int Score = 0;
		private int delayMove = 600;
		private TextView textScore;
		private bool playing = false;
		private Direction mDirection = Direction.East;
		private List<Coordinates> snake = new List<Coordinates> ();
		private List<Coordinates> apple = new List<Coordinates> ();
		private DrawObjects drawObjects = new DrawObjects ();
		private static int x = 0;
		private static int y = 0;
		private static Random random = new Random ();

		public TextView TextScore {
			set{ textScore = value; }
		}

		#endregion

		#region Constructors

		public NormalGameView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			GraphicMode = false;
			InitGameView ();
			screenEvents = new ScreenEvents (this);
		}

		public NormalGameView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			GraphicMode = false;
			InitGameView ();
			screenEvents = new ScreenEvents (this);
		}

		#endregion

		#region Methods

		private void InitGameView ()
		{
			Focusable = true;
			FocusableInTouchMode = true;
			Click += new EventHandler (GameView_onClick);
		}

		private void InitNewGame ()
		{
			x = 120;
			y = 120;	
			xMax = 20 * 12;
			yMax = 25 * 12;
			snake.Clear ();
			apple.Clear ();
			snake.Add (new Coordinates (x, y));
			snake.Add (new Coordinates (x - 12, y));
			snake.Add (new Coordinates (x - 24, y));
			snake.Add (new Coordinates (x - 24, y - 12));
			snake.Add (new Coordinates (x - 24, y - 24));
			BuildApples ();
			textScore.Text = "Score: 0";
		}

		public void BuildWorld ()
		{
			if (playing) {
				BuildSnake ();
				BuildWalls ();
			} else
				textScore.Text = "Game Over... Touch for play again!";
			screenEvents.Delay (delayMove);
		}

		private void BuildWalls ()
		{
			_wallPoints = new List<PointF[]> ();
			Coordinates _position;
			for (int x = 0; x < xMax; x += 12) {
				_position = new Coordinates (x, 0);
				_wallPoints.Add (drawObjects.wall (_position));
				_position = new Coordinates (x, yMax - 12);
				_wallPoints.Add (drawObjects.wall (_position));
			}

			for (int y = 1; y < yMax; y += 12) {
				_position = new Coordinates (0, y);
				_wallPoints.Add (drawObjects.wall (_position));
				_position = new Coordinates (xMax - 12, y);
				_wallPoints.Add (drawObjects.wall (_position));
			}
		}

		private void BuildApples ()
		{
			Coordinates newApple = null;
			bool collision = true;
			while (collision) {
				int newX = 1;
				int newY = 1;
				while (newX % 12 != 0)
					newX = 12 + random.Next (xMax - 24);
				while (newY % 12 != 0)
					newY = 12 + random.Next (yMax - 24);
				newApple = new Coordinates (newX, newY);
				collision = false;
				foreach (Coordinates position in snake) {
					if (position == newApple)
						collision = true;
				}
			}
			apple.Add (newApple);
			_applePoints.Add (drawObjects.apple (newApple));
		}

		private void BuildSnake ()
		{
			bool isGrowing = false;
			var textScoreContent = "";
			_snakePoints = new List<PointF[]> ();
			foreach (Coordinates _apple in apple) {
				if (_apple.X.Equals (snake [0].X) && _apple.Y.Equals (snake [0].Y)) {
					apple.Remove (_apple);
					_applePoints = new List<PointF[]> ();
					foreach (Coordinates updatedApples in apple) {
						_applePoints.Add (drawObjects.apple (updatedApples));
					}
					BuildApples ();
					Score++;
					textScoreContent = string.Format ("Score: {0} ", Score.ToString ());
					textScore.Text = textScoreContent;
					delayMove -= 20;
					isGrowing = true;
					break;
				}
			}

			switch (mDirection) {
			case Direction.East:
				x += 12;
				snake.Insert (0, new Coordinates (x, y));
				_snakePoints.Add (drawObjects.head (Direction.East, snake [0]));
				break;
			case Direction.West:
				x -= 12;
				snake.Insert (0, new Coordinates (x, y));
				_snakePoints.Add (drawObjects.head (Direction.West, snake [0]));
				break;
			case Direction.North:
				y -= 12;
				snake.Insert (0, new Coordinates (x, y));
				_snakePoints.Add (drawObjects.head (Direction.North, snake [0]));
				break;
			case Direction.South:
				y += 12;
				snake.Insert (0, new Coordinates (x, y));
				_snakePoints.Add (drawObjects.head (Direction.South, snake [0]));
				break;
			}

			if ((snake [0].X < 12) || (snake [0].Y < 12) || (snake [0].X > xMax - 24)
			    || (snake [0].Y > yMax - 24)) {
				playing = false;
				_applePoints.Clear ();
				_snakePoints.Clear ();
				return;
			}

			bool isHead = true;
			foreach (Coordinates part in snake) {
				if (isHead)
					isHead = false;
				else if (part.X.Equals (snake [0].X) && part.Y.Equals (snake [0].Y)) {
					playing = false;
					_snakePoints.Clear ();
					_applePoints.Clear ();
					return;
				}
			}

			if (!isGrowing)
				snake.Remove (snake [snake.Count - 1]);

			foreach (Coordinates part in snake) {
				if (!part.X.Equals (snake [0].X) || !part.Y.Equals (snake [0].Y))
					_snakePoints.Add (drawObjects.body (part));
			}
		}

		#endregion

		#region Events

		public override bool OnKeyDown (Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
		{
			switch (keyCode) {
			case Keycode.DpadUp: 
				if (mDirection != Direction.South)
					mDirection = Direction.North;
				break;
			case Keycode.DpadDown:
				if (mDirection != Direction.North)
					mDirection = Direction.South;
				break;
			case Keycode.DpadLeft:
				if (mDirection != Direction.East)
					mDirection = Direction.West;
				break;
			case Keycode.DpadRight:
				if (mDirection != Direction.West)
					mDirection = Direction.East;
				break;
			}
			return base.OnKeyDown (keyCode, e);
		}

		private void GameView_onClick (object sender, EventArgs e)
		{
			bool isHead = true;
			_snakePoints = new List<PointF[]> ();
			_applePoints = new List<PointF[]> ();
			InitNewGame ();
			foreach (Coordinates part in snake) {
				if (isHead) {
					_snakePoints.Add (drawObjects.head (Direction.East, part));
					isHead = false;
				} else {
					_snakePoints.Add (drawObjects.body (part));
				}
			}
			playing = true;
			BuildWorld ();
		}

		#endregion
	}
}