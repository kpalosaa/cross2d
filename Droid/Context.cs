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

		public void Clear()
		{
			canvas.DrawPaint(paint);
		}

		public void DrawRect(float x, float y, float width, float height)
		{
			canvas.DrawRect(x, y, x + width - 1, y + height - 1, paint);
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			canvas.DrawLine(x1, y1, x2, y2, paint);
		}

		public void DrawCircle(float xCenter, float yCenter, float radius)
		{
			canvas.DrawCircle(xCenter, yCenter, radius, paint);
		}

		public void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			canvas.DrawOval(xCenter - hRadius, yCenter - vRadius, xCenter + hRadius, yCenter + vRadius, paint);
		}

		public void SetFont(string name, int size, FontStyle style)
		{
			TypefaceStyle typefaceStyle = TypefaceStyle.Normal;

			if ((style & (FontStyle.Bold | FontStyle.Italic)) != 0)
				typefaceStyle = TypefaceStyle.BoldItalic;
			else if ((style & FontStyle.Bold) != 0)
				typefaceStyle = TypefaceStyle.Bold;
			else if ((style & FontStyle.Italic) != 0)
				typefaceStyle = TypefaceStyle.Italic;

			paint.SetTypeface(Typeface.Create(name, typefaceStyle));
			paint.TextSize = size;
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			return new Xamarin.Forms.Size(paint.MeasureText(text), paint.FontSpacing);
		}

		public void DrawText(string text, float x, float y)
		{
			canvas.DrawText(text, x, y, paint);
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
			float textWidth = paint.MeasureText(text);
			float textHeight = paint.FontSpacing;

			if ((hAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				x += (width - textWidth) / 2;
			else if ((hAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				x += width - textWidth;

			if ((vAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				y += (height - textHeight) / 2;
			else if ((vAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				y += height - textHeight;

			canvas.DrawText(text, x, y, paint);
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

		public StrokeStyle StrokeStyle
		{
			get { return strokeStyles[paint.GetStyle()]; }
			set { paint.SetStyle(strokeStyles[value]); }
		}

		public CapStyle StrokeCapStyle
		{
			get	{ return capStyles[paint.StrokeCap]; }
			set { paint.StrokeCap = capStyles[value]; }
		}

		public JoinStyle StrokeJoinStyle
		{
			get { return joinStyles[paint.StrokeJoin]; }
			set { paint.StrokeJoin = joinStyles[value]; }
		}

		public float Width { get; internal set; }
		public float Height { get; internal set; }

		private static readonly BiDictionary<StrokeStyle, Paint.Style> strokeStyles = new BiDictionary<StrokeStyle, Paint.Style>()
		{
			[StrokeStyle.Fill] = Paint.Style.Fill,
			[StrokeStyle.Stroke] = Paint.Style.Stroke,
			[StrokeStyle.StrokeAndFill] = Paint.Style.FillAndStroke
		};

		private static readonly BiDictionary<CapStyle, Paint.Cap> capStyles = new BiDictionary<CapStyle, Paint.Cap>()
		{
			[CapStyle.Flat] = Paint.Cap.Butt,
			[CapStyle.Round] = Paint.Cap.Round,
			[CapStyle.Square] = Paint.Cap.Square
		};

		private static readonly BiDictionary<JoinStyle, Paint.Join> joinStyles = new BiDictionary<JoinStyle, Paint.Join>()
		{
			[JoinStyle.Bevel] = Paint.Join.Bevel,
			[JoinStyle.Miter] = Paint.Join.Miter,
			[JoinStyle.Round] = Paint.Join.Round
		};
	}
}
