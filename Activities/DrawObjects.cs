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
	public class DrawObjects
	{
		public PointF[] wall (Coordinates _position)
		{
			PointF[] _points = new[] {
				new PointF (_position.X, _position.Y),
				new PointF (_position.X + 12, _position.Y),
				new PointF (_position.X + 12, _position.Y + 12),
				new PointF (_position.X, _position.Y + 12),
				new PointF (_position.X, _position.Y)
			};
			return _points;
		}

		public PointF[] apple (Coordinates _position)
		{
			PointF[] _points = new[] {
				new PointF (_position.X, _position.Y + 2),
				new PointF (_position.X + 2, _position.Y),
				new PointF (_position.X + 3, _position.Y),
				new PointF (_position.X + 6, _position.Y + 3),
				new PointF (_position.X + 9, _position.Y),
				new PointF (_position.X + 10, _position.Y),
				new PointF (_position.X + 12, _position.Y + 2),
				new PointF (_position.X + 12, _position.Y + 10),
				new PointF (_position.X + 10, _position.Y + 12),
				new PointF (_position.X + 2, _position.Y + 12),
				new PointF (_position.X, _position.Y + 10),
				new PointF (_position.X, _position.Y + 2)
			};
			return _points;
		}

		public PointF[] head (Direction compass, Coordinates _position)
		{			 
			PointF[] _points;
			switch (compass) {
			case Direction.East:
				_points = new[] {
					new PointF (_position.X, _position.Y),
					new PointF (_position.X + 8, _position.Y),
					new PointF (_position.X + 12, _position.Y + 6),
					new PointF (_position.X + 8, _position.Y + 12),
					new PointF (_position.X, _position.Y + 12),
					new PointF (_position.X, _position.Y)
				};
				break;
			case Direction.West:
				_points = new[] {
					new PointF (_position.X, _position.Y + 6),
					new PointF (_position.X + 4, _position.Y),
					new PointF (_position.X + 12, _position.Y),
					new PointF (_position.X + 12, _position.Y + 12),
					new PointF (_position.X + 4, _position.Y + 12),
					new PointF (_position.X, _position.Y + 6)
				};
				break;
			case Direction.North:
				_points = new[] {
					new PointF (_position.X + 6, _position.Y),
					new PointF (_position.X + 12, _position.Y + 4),
					new PointF (_position.X + 12, _position.Y + 12),
					new PointF (_position.X, _position.Y + 12),
					new PointF (_position.X, _position.Y + 4),
					new PointF (_position.X + 6, _position.Y)
				};
				break;
			case Direction.South:
				_points = new[] {
					new PointF (_position.X, _position.Y),
					new PointF (_position.X + 12, _position.Y),
					new PointF (_position.X + 12, _position.Y + 8),
					new PointF (_position.X + 6, _position.Y + 12),
					new PointF (_position.X, _position.Y + 8),
					new PointF (_position.X, _position.Y)
				};
				break;
			default:
				_points = new[] {
					new PointF (_position.X + 6, _position.Y),
					new PointF (_position.X + 12, _position.Y + 4),
					new PointF (_position.X + 12, _position.Y + 12),
					new PointF (_position.X, _position.Y + 12),
					new PointF (_position.X, _position.Y + 4),
					new PointF (_position.X + 6, _position.Y)
				};
				break;
			}
			return _points;
		}

		public PointF[] body (Coordinates _position)
		{
			PointF[] _points;
			_points = new[] {
				new PointF (_position.X, _position.Y),
				new PointF (_position.X + 12, _position.Y),
				new PointF (_position.X + 12, _position.Y + 12),
				new PointF (_position.X, _position.Y + 12),
				new PointF (_position.X, _position.Y)
			};
			return _points;
		}
	}
}