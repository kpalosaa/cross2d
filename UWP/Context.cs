using System;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;

namespace Uni2D
{
	public class Context : IContext
	{
		private CanvasControl canvas;
		private CanvasDrawingSession ds;

		private CanvasStrokeStyle strokeStyle = new CanvasStrokeStyle();
		private CanvasSolidColorBrush solidColorBrush;
		private float strokeWidth;

		internal Context(CanvasControl canvas, CanvasDrawingSession ds)
		{
			this.canvas = canvas;
			this.ds = ds;

			strokeStyle = new CanvasStrokeStyle();
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			ds.DrawLine(x1, y1, x2, y2, solidColorBrush, strokeWidth, strokeStyle);
		}

		public void DrawCircle(float x, float y, float radius)
		{
			ds.DrawCircle(x, y, radius, solidColorBrush, strokeWidth, strokeStyle);
		}

		public Xamarin.Forms.Color Color
		{
			get { return Xamarin.Forms.Color.FromHex(solidColorBrush.Color.ToString()); }
			set { solidColorBrush = new CanvasSolidColorBrush(canvas, value.ToWin2dColor()); }
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




		private static readonly BiDictionary<CapStyle, CanvasCapStyle> capStyles = new BiDictionary<CapStyle, CanvasCapStyle>()
		{
			[CapStyle.Flat] = CanvasCapStyle.Flat,
			[CapStyle.Round] = CanvasCapStyle.Round,
			[CapStyle.Square] = CanvasCapStyle.Square
		};
	}

	public static class Extensions
	{
		public static Windows.UI.Color ToWin2dColor(this Xamarin.Forms.Color color)
		{
			return Windows.UI.Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
		}
	}

}
