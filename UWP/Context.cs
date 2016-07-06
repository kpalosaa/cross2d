using System;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Windows.Foundation;
using Windows.UI;

namespace Uni2D
{
	public class Context : IContext
	{
		private CanvasControl canvas;
		private CanvasDrawingSession ds;

		private CanvasStrokeStyle strokeStyle = new CanvasStrokeStyle();
		private Color color;
		private float strokeWidth = 1;
		private CanvasTextFormat textFormat = new CanvasTextFormat();
		private string defaultFontFamily;

		internal Context(CanvasControl canvas, CanvasDrawingSession ds)
		{
			this.canvas = canvas;
			this.ds = ds;

			defaultFontFamily = textFormat.FontFamily;
		}

		public void Clear()
		{
			ds.Clear(color);
		}

		public void DrawRect(float x, float y, float width, float height)
		{
			ds.DrawRectangle(x, y, width, height, color, strokeWidth, strokeStyle);
		}

		public void FillRect(float x, float y, float width, float height)
		{
			ds.FillRectangle(x, y, width, height, color);
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			ds.DrawLine(x1, y1, x2, y2, color, strokeWidth, strokeStyle);
		}

		public void DrawCircle(float x, float y, float radius)
		{
			ds.DrawCircle(x, y, radius, color, strokeWidth, strokeStyle);
		}

		public void FillCircle(float x, float y, float radius)
		{
			ds.FillCircle(x, y, radius, color);
		}

		public void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			ds.DrawEllipse(xCenter, yCenter, hRadius, vRadius, color, strokeWidth, strokeStyle);
		}

		public void FillEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			ds.FillEllipse(xCenter, yCenter, hRadius, vRadius, color);
		}

		public void SetFont(string name, int size, FontStyle style)
		{
			SetFontStyle(style);

			textFormat.FontFamily = name;
			textFormat.FontSize = size;
		}

		public void SetFont(int size, FontStyle style = 0)
		{
			SetFontStyle(style);

			textFormat.FontFamily = defaultFontFamily;
			textFormat.FontSize = size;
		}

		public void SetFont(Xamarin.Forms.NamedSize namedSize, FontStyle style = 0)
		{
			SetFontStyle(style);

			textFormat.FontFamily = defaultFontFamily;
			textFormat.FontSize = (float)Xamarin.Forms.Device.GetNamedSize(namedSize, typeof(Xamarin.Forms.Label));
		}

		private void SetFontStyle(FontStyle style)
		{
			if ((style & FontStyle.Bold) != 0)
				textFormat.FontWeight = Windows.UI.Text.FontWeights.Bold;
			else
				textFormat.FontWeight = Windows.UI.Text.FontWeights.Normal;

			if ((style & FontStyle.Italic) != 0)
				textFormat.FontStyle |= Windows.UI.Text.FontStyle.Italic;
			else
				textFormat.FontStyle = 0;
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			CanvasTextLayout textLayout = new CanvasTextLayout(ds, text, textFormat, 0.0f, 0.0f);
			Xamarin.Forms.Size size = new Xamarin.Forms.Size(textLayout.DrawBounds.Width, textLayout.DrawBounds.Height);
			textLayout.Dispose();

			return size;
		}

		public void DrawText(string text, float x, float y)
		{
			textFormat.HorizontalAlignment = CanvasHorizontalAlignment.Left;
			textFormat.VerticalAlignment = CanvasVerticalAlignment.Top;

			ds.DrawText(text, x, y, color, textFormat);
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
			switch (hAlignment)
			{
				case Xamarin.Forms.TextAlignment.Start:	textFormat.HorizontalAlignment = CanvasHorizontalAlignment.Left; break;
				case Xamarin.Forms.TextAlignment.Center: textFormat.HorizontalAlignment = CanvasHorizontalAlignment.Center; break;
				case Xamarin.Forms.TextAlignment.End: textFormat.HorizontalAlignment = CanvasHorizontalAlignment.Right; break;
			}

			switch (vAlignment)
			{
				case Xamarin.Forms.TextAlignment.Start: textFormat.VerticalAlignment = CanvasVerticalAlignment.Top; break;
				case Xamarin.Forms.TextAlignment.Center: textFormat.VerticalAlignment = CanvasVerticalAlignment.Center; break;
				case Xamarin.Forms.TextAlignment.End: textFormat.VerticalAlignment = CanvasVerticalAlignment.Bottom; break;
			}

			ds.DrawText(text, x, y, width, height, color, textFormat);
		}

		public Xamarin.Forms.Color Color
		{
			get { return Xamarin.Forms.Color.FromHex(color.ToString()); }
			set { color = value.ToWin2dColor(); }
		}

		public float StrokeWidth
		{
			get { return strokeWidth; }
			set { strokeWidth = value; }
		}

		public CapStyle StrokeCapStyle
		{
			get
			{
				return capStyles[strokeStyle.StartCap];
			}
			set
			{
				strokeStyle.StartCap = strokeStyle.EndCap = capStyles[value];
			}
		}

		public JoinStyle StrokeJoinStyle { get; set; }

		public float Width { get { return (float)canvas.Size.Width; } }
		public float Height { get { return (float)canvas.Size.Height; } }

		private static readonly BiDictionary<CapStyle, CanvasCapStyle> capStyles = new BiDictionary<CapStyle, CanvasCapStyle>()
		{
			[CapStyle.Flat] = CanvasCapStyle.Flat,
			[CapStyle.Round] = CanvasCapStyle.Round,
			[CapStyle.Square] = CanvasCapStyle.Square
		};
	}

	public static class Extensions
	{
		public static Color ToWin2dColor(this Xamarin.Forms.Color color)
		{
			return Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
		}
	}
}
