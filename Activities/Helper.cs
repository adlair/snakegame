using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;

namespace SnakeGame.Activities
{
	public class Helper : View
	{
		#region Variables and Properties

		protected static int objectSize = 10;
		protected static int drawSize = 25;
		protected static int xMax;
		protected static int yMax;
		private static int xBalance;
		private static int yBalance;
		private Bitmap[] bitmaps;
		private GameObjects[,] gameObjects;
		private Paint paint = new Paint ();

		public bool GraphicMode {
			get;
			set;
		}

		public List<PointF[]> _applePoints {
			get;
			set;
		}

		public List<PointF[]> _wallPoints {
			get;
			set;
		}

		public List<PointF[]> _snakePoints {
			get;
			set;
		}

		#endregion

		#region Constructors

		public Helper (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
		}

		public Helper (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
		}

		#endregion

		#region Methods

		public void ResetTiles (int tileCount)
		{
			bitmaps = new Bitmap[tileCount];
		}

		public void LoadObject (GameObjects type, Drawable tile)
		{
			Bitmap bitmap = Bitmap.CreateBitmap (objectSize, objectSize, Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas (bitmap);

			tile.SetBounds (0, 0, objectSize, objectSize);
			tile.Draw (canvas);

			bitmaps [(int)type] = bitmap;
		}

		public void ClearObjects ()
		{
			for (int x = 0; x < xMax; x++)
				for (int y = 0; y < yMax; y++)
					SetObject (0, x, y);
		}

		public void SetObject (GameObjects tile, int x, int y)
		{
			gameObjects [x, y] = tile;
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			if (GraphicMode) {
				xMax = (int)System.Math.Floor ((double)w / objectSize);
				yMax = (int)System.Math.Floor ((double)h / objectSize);

				xBalance = ((w - (objectSize * xMax)) / 2);
				yBalance = ((h - (objectSize * yMax)) / 2);

				gameObjects = new GameObjects[xMax, yMax];

				ClearObjects ();
			} else {
				xMax = (int)System.Math.Floor ((double)w / drawSize);
				yMax = (int)System.Math.Floor ((double)h / drawSize);

				xBalance = 0;
				yBalance = 0;
			}
		}

		#endregion

		#region Events

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			if (GraphicMode) {
				for (int x = 0; x < xMax; x += 1)
					for (int y = 0; y < yMax; y += 1)
						if (gameObjects [x, y] > 0)
							canvas.DrawBitmap (bitmaps [(int)gameObjects [x, y]],
								xBalance + x * objectSize,
								yBalance + y * objectSize,
								paint);
			} else {
				if (_wallPoints != null) {
					var path = new Path ();
					foreach (PointF[] _point in _wallPoints) {
						path.MoveTo (_point [0].X, _point [0].Y);
						for (var i = 1; i < _point.Length; i++) {
							path.LineTo (_point [i].X, _point [i].Y);
						}
					}
					var paint = new Paint {
						Color = Color.Brown
					};
					paint.SetStyle (Paint.Style.Fill);
					canvas.DrawPath (path, paint);
				}

				if (_applePoints != null) {
					var path = new Path ();
					foreach (PointF[] _point in _applePoints) {
						path.MoveTo (_point [0].X, _point [0].Y);
						for (var i = 1; i < _point.Length; i++) {
							path.LineTo (_point [i].X, _point [i].Y);
						}
					}
					var paint = new Paint {
						Color = Color.Red
					};
					paint.SetStyle (Paint.Style.Fill);
					canvas.DrawPath (path, paint);
				}

				if (_snakePoints != null) {
					var path = new Path ();
					foreach (PointF[] _point in _snakePoints) {
						path.MoveTo (_point [0].X, _point [0].Y);
						for (var i = 1; i < _point.Length; i++) {
							path.LineTo (_point [i].X, _point [i].Y);
						}
					}
					var paint = new Paint {
						Color = Color.LawnGreen
					};
					paint.SetStyle (Paint.Style.Fill);
					canvas.DrawPath (path, paint);
				}
			}
		}

		#endregion
	}
}