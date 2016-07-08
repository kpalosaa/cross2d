using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Uni2D
{
	public sealed class Context : IContext
	{
		private Canvas canvas;
		private Paint paintStroke;
		private Paint paintFill;
		private Rect rect;

		internal Context(Canvas canvas, Rect rect)
		{
			this.canvas = canvas;
			this.rect = rect;

			paintStroke = new Paint();
			paintStroke.SetStyle(Paint.Style.Stroke);

			paintFill = new Paint();
			paintFill.SetStyle(Paint.Style.Fill);
		}

		public void Clear()
		{
			canvas.DrawPaint(paintFill);
		}

		public void DrawRect(float x, float y, float width, float height)
		{
			canvas.DrawRect(x, y, x + width - 1, y + height - 1, paintStroke);
		}

		public void FillRect(float x, float y, float width, float height)
		{
			canvas.DrawRect(x, y, x + width - 1, y + height - 1, paintFill);
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			canvas.DrawLine(x1, y1, x2, y2, paintStroke);
		}

		public void DrawCircle(float xCenter, float yCenter, float radius)
		{
			canvas.DrawCircle(xCenter, yCenter, radius, paintStroke);
		}

		public void FillCircle(float xCenter, float yCenter, float radius)
		{
			canvas.DrawCircle(xCenter, yCenter, radius, paintFill);
		}

		public void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			canvas.DrawOval(xCenter - hRadius, yCenter - vRadius, xCenter + hRadius, yCenter + vRadius, paintStroke);
		}

		public void FillEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			canvas.DrawOval(xCenter - hRadius, yCenter - vRadius, xCenter + hRadius, yCenter + vRadius, paintFill);
		}

		public void SetFont(string name, int size, FontStyle style)
		{
			paintFill.SetTypeface(Typeface.Create(name, GetFontStyle(style)));
			paintFill.TextSize = size;
		}

		public void SetFont(int size, FontStyle style = 0)
		{
			paintFill.SetTypeface(Typeface.DefaultFromStyle(GetFontStyle(style)));
			paintFill.TextSize = size;
		}

		public void SetFont(Xamarin.Forms.NamedSize namedSize, FontStyle style = 0)
		{
			paintFill.SetTypeface(Typeface.DefaultFromStyle(GetFontStyle(style)));
			paintFill.TextSize = (float)Xamarin.Forms.Device.GetNamedSize(namedSize, typeof(Xamarin.Forms.Label));
		}

		private TypefaceStyle GetFontStyle(FontStyle style)
		{
			TypefaceStyle typefaceStyle = TypefaceStyle.Normal;

			if ((style & (FontStyle.Bold | FontStyle.Italic)) != 0)
				typefaceStyle = TypefaceStyle.BoldItalic;
			else if ((style & FontStyle.Bold) != 0)
				typefaceStyle = TypefaceStyle.Bold;
			else if ((style & FontStyle.Italic) != 0)
				typefaceStyle = TypefaceStyle.Italic;

			return typefaceStyle;
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			return new Xamarin.Forms.Size(paintFill.MeasureText(text), paintFill.FontSpacing);
		}

		public void DrawText(string text, float x, float y)
		{
			canvas.DrawText(text, x, y - paintFill.GetFontMetrics().Ascent, paintFill);
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
			float textWidth = paintFill.MeasureText(text);
			float textHeight = paintFill.FontSpacing;

			if ((hAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				x += (width - textWidth) / 2;
			else if ((hAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				x += width - textWidth;

			if ((vAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				y += (height - textHeight) / 2;
			else if ((vAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				y += height - textHeight;

			canvas.DrawText(text, x, y - paintFill.GetFontMetrics().Ascent, paintFill);
		}

		public void DrawPath(IPath path, float x, float y)
		{
			canvas.Save();
			canvas.Translate(x, y);
			canvas.DrawPath(((Path)path).NativePath, paintStroke);
			canvas.Restore();
		}

		public void FillPath(IPath path, float x, float y)
		{
			canvas.Save();
			canvas.Translate(x, y);
			canvas.DrawPath(((Path)path).NativePath, paintFill);
			canvas.Restore();
		}

		public void Save()
		{
			canvas.Save();
		}

		public void Restore()
		{
			canvas.Restore();
		}

		public void Translate(float dx, float dy)
		{
			canvas.Translate(dx, dy);
		}

		public void Scale(float sx, float sy)
		{
			canvas.Scale(sx, sy);
		}

		public void Rotate(float angle)
		{
			canvas.Rotate(angle * MathHelper.RadToDeg);
		}

		public Xamarin.Forms.Color Color
		{
			get { return Xamarin.Forms.Color.FromUint((uint)paintStroke.Color.ToArgb()); }
			set { paintStroke.Color = paintFill.Color = value.ToAndroid(); }
		}

		public float StrokeWidth
		{
			get { return paintStroke.StrokeWidth; }
			set { paintStroke.StrokeWidth = value; }
		}

		public CapStyle StrokeCapStyle
		{
			get	{ return capStyles[paintStroke.StrokeCap]; }
			set { paintStroke.StrokeCap = capStyles[value]; }
		}

		public JoinStyle StrokeJoinStyle
		{
			get { return joinStyles[paintStroke.StrokeJoin]; }
			set { paintStroke.StrokeJoin = joinStyles[value]; }
		}

		public float Width { get { return canvas.Width; } }
		public float Height { get { return canvas.Height; } }

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
