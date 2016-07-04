using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Uni2D
{
	public sealed class Context : IContext
	{
		private Canvas canvas;
		private Paint paint;
		private Rect rect;

		internal Context(Canvas canvas, Paint paint, Rect rect)
		{
			this.canvas = canvas;
			this.paint = paint;
			this.rect = rect;
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			canvas.DrawLine(x1, y1, x2, y2, paint);
		}

		public void DrawCircle(float x, float y, float radius)
		{
			canvas.DrawCircle(x, y, radius, paint);
		}

		public Xamarin.Forms.Color Color
		{
			get { return Xamarin.Forms.Color.FromUint((uint)paint.Color.ToArgb()); }
			set { paint.Color = value.ToAndroid(); }
		}

		public float StrokeWidth
		{
			get { return paint.StrokeWidth; }
			set { paint.StrokeWidth = value; }
		}

		public CapStyle StrokeCapStyle
		{
			get
			{
				return capStyles[paint.StrokeCap];
			}
			set
			{
				paint.StrokeCap = capStyles[value];
			}
		}




		private static readonly BiDictionary<CapStyle, Paint.Cap> capStyles = new BiDictionary<CapStyle, Paint.Cap>()
		{
			[CapStyle.Flat] = Paint.Cap.Butt,
			[CapStyle.Round] = Paint.Cap.Round,
			[CapStyle.Square] = Paint.Cap.Square
		};

	}
}
