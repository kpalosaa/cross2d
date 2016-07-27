using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Windows.Foundation;
using Windows.UI;

namespace Cross2D
{
	public class Context : IContext
	{
		private CanvasControl canvas;
		internal CanvasDrawingSession ds;

		private CanvasStrokeStyle strokeStyle = new CanvasStrokeStyle();
		private Color color;
		private float strokeWidth = 1;
		private CanvasTextFormat defaultTextFormat = new CanvasTextFormat();

		private IFont font;

		private Stack<Matrix3x2> stateStack;

		internal Context(CanvasControl canvas)
		{
			this.canvas = canvas;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				strokeStyle.Dispose();
				defaultTextFormat.Dispose();
			}
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

		public void SetFont(IFont font)
		{
			this.font = font;
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			CanvasTextFormat textFormat = font != null ? ((Font)font).NativeTextFormat : defaultTextFormat;

			CanvasTextLayout textLayout = new CanvasTextLayout(ds, text, textFormat, Single.PositiveInfinity, Single.PositiveInfinity);
			Xamarin.Forms.Size size = new Xamarin.Forms.Size(textLayout.LayoutBounds.Width, textLayout.LayoutBounds.Height);
			textLayout.Dispose();

			return size;
		}

		public void DrawText(string text, float x, float y)
		{
			CanvasTextFormat textFormat = font != null ? ((Font)font).NativeTextFormat : defaultTextFormat;

			textFormat.HorizontalAlignment = CanvasHorizontalAlignment.Left;
			textFormat.VerticalAlignment = CanvasVerticalAlignment.Top;

			ds.DrawText(text, x, y, color, textFormat);
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
			CanvasTextFormat textFormat = font != null ? ((Font)font).NativeTextFormat : defaultTextFormat;

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

		public void DrawPath(IPath path, float x, float y)
		{
			ds.DrawGeometry(((Path)path).GetGeometry(), x, y, color, strokeWidth, strokeStyle);
		}

		public void FillPath(IPath path, float x, float y)
		{
			ds.FillGeometry(((Path)path).GetGeometry(), x, y, color);
		}

		public void DrawImage(IImage image, float x, float y, float width, float height)
		{
			if (image != null)
			{
				ds.DrawImage(((Image)image).NativeBitmap, new Rect(x, y, width, height));
			}
		}

		public void Save()
		{
			if (stateStack == null)
				stateStack = new Stack<Matrix3x2>();

			stateStack.Push(ds.Transform);
		}

		public void Restore()
		{
			if (stateStack != null && stateStack.Count > 0)
				ds.Transform = stateStack.Pop();
		}

		public void Translate(float dx, float dy)
		{
			ds.Transform *= Matrix3x2.CreateTranslation(dx, dy);
		}

		public void Scale(float sx, float sy)
		{
			ds.Transform *= Matrix3x2.CreateScale(sx, sy);
		}

		public void Rotate(float angle)
		{
			ds.Transform *= Matrix3x2.CreateRotation(angle);
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

		public JoinStyle StrokeJoinStyle
		{
			get
			{
				return joinStyles[strokeStyle.LineJoin];
			}
			set
			{
				strokeStyle.LineJoin = joinStyles[value];
			}
		}

		public float Width { get { return (float)canvas.Size.Width; } }
		public float Height { get { return (float)canvas.Size.Height; } }

		private static readonly BiDictionary<CapStyle, CanvasCapStyle> capStyles = new BiDictionary<CapStyle, CanvasCapStyle>()
		{
			[CapStyle.Flat] = CanvasCapStyle.Flat,
			[CapStyle.Round] = CanvasCapStyle.Round,
			[CapStyle.Square] = CanvasCapStyle.Square
		};

		private static readonly BiDictionary<JoinStyle, CanvasLineJoin> joinStyles = new BiDictionary<JoinStyle, CanvasLineJoin>()
		{
			[JoinStyle.Bevel] = CanvasLineJoin.Bevel,
			[JoinStyle.Miter] = CanvasLineJoin.Miter,
			[JoinStyle.Round] = CanvasLineJoin.Round
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
