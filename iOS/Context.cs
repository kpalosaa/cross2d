using System;
using CoreGraphics;
using Xamarin.Forms.Platform.iOS;

namespace Uni2D
{
	public class Context : IContext
	{
		private CGContext context;
		private CGRect rect;

		private CGLineCap lineCap;
		private CGLineJoin lineJoin;
		private float lineWidth;
		private CGColor color;

		public Context(CGContext context, CGRect rect)
		{
			this.context = context;
			this.rect = rect;
		}

		public void Clear()
		{
		}

		public void DrawRect(float x, float y, float width, float height)
		{
		}

		public void FillRect(float x, float y, float width, float height)
		{
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
		}

		public void DrawCircle(float xCenter, float yCenter, float radius)
		{
		}

		public void FillCircle(float xCenter, float yCenter, float radius)
		{
		}

		public void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
		}

		public void FillEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
		}

		public void SetFont(string name, int size, FontStyle style)
		{
		}

		public void SetFont(int size, FontStyle style = 0)
		{
		}

		public void SetFont(Xamarin.Forms.NamedSize namedSize, FontStyle style = 0)
		{
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			return Xamarin.Forms.Size.Zero;
		}

		public void DrawText(string text, float x, float y)
		{
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
		}

		public Xamarin.Forms.Color Color
		{
			get { return Xamarin.Forms.Color.Transparent; }
			set { color = value.ToCGColor(); context.SetFillColor(color); context.SetStrokeColor(color); }
		}

		public float StrokeWidth
		{
			get { return lineWidth; }
			set { lineWidth = value; context.SetLineWidth(lineWidth); }
		}

		public CapStyle StrokeCapStyle
		{
			get { return capStyles[lineCap]; }
			set { lineCap = capStyles[value]; context.SetLineCap(lineCap); }
		}

		public JoinStyle StrokeJoinStyle
		{
			get { return joinStyles[lineJoin]; }
			set { lineJoin = joinStyles[value]; context.SetLineJoin(lineJoin); }
		}

		public float Width { get; internal set; }
		public float Height { get; internal set; }

		private static readonly BiDictionary<CapStyle, CGLineCap> capStyles = new BiDictionary<CapStyle, CGLineCap>()
		{
			[CapStyle.Flat] = CGLineCap.Butt,
			[CapStyle.Round] = CGLineCap.Round,
			[CapStyle.Square] = CGLineCap.Square
		};

		private static readonly BiDictionary<JoinStyle, CGLineJoin> joinStyles = new BiDictionary<JoinStyle, CGLineJoin>()
		{
			[JoinStyle.Bevel] = CGLineJoin.Bevel,
			[JoinStyle.Miter] = CGLineJoin.Miter,
			[JoinStyle.Round] = CGLineJoin.Round
		};
	}
}
