using System;
using CoreGraphics;
using CoreText;
using Foundation;
using Xamarin.Forms.Platform.iOS;

namespace Cross2D
{
	public class Context : IContext
	{
		private NativeView view;

		internal CGContext context;
		internal CGRect rect;

		private CGLineCap lineCap;
		private CGLineJoin lineJoin;
		private float lineWidth;
		private Xamarin.Forms.Color color;

		private Font font;
		private CGPoint[] lineSegment;

		public Context(NativeView view)
		{
			this.view = view;

			lineSegment = new CGPoint[] { new CGPoint(), new CGPoint() };
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
			}
		}

		public void Clear()
		{
			context.FillRect(rect);
		}

		public void DrawRect(float x, float y, float width, float height)
		{
			context.StrokeRect(new CGRect(x, y, width, height));
		}

		public void FillRect(float x, float y, float width, float height)
		{
			context.FillRect(new CGRect(x, y, width, height));
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			lineSegment[0].X = x1;
			lineSegment[0].Y = y1;
			lineSegment[1].X = x2;
			lineSegment[1].Y = y2;

			context.StrokeLineSegments(lineSegment);
		}

		public void DrawCircle(float xCenter, float yCenter, float radius)
		{
			context.StrokeEllipseInRect(new CGRect(xCenter - radius, yCenter - radius, 2 * radius, 2 * radius));
		}

		public void FillCircle(float xCenter, float yCenter, float radius)
		{
			context.FillEllipseInRect(new CGRect(xCenter - radius, yCenter - radius, 2 * radius, 2 * radius));
		}

		public void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			context.StrokeEllipseInRect(new CGRect(xCenter - hRadius, yCenter - vRadius, 2 * hRadius, 2 * vRadius));
		}

		public void FillEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			context.FillEllipseInRect(new CGRect(xCenter - hRadius, yCenter - vRadius, 2 * hRadius, 2 * vRadius));
		}

		public void SetFont(IFont font)
		{
			this.font = (Font)font;
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			var attributedString = new NSAttributedString(text, new CTStringAttributes() { Font = font.NativeFont, ForegroundColorFromContext = true });
			CTLine ctLine = new CTLine(attributedString);
	
			Xamarin.Forms.Size size = new Xamarin.Forms.Size((float)ctLine.GetTypographicBounds(), font.NativeFont.CapHeightMetric + font.NativeFont.DescentMetric);

			ctLine.Dispose();
			attributedString.Dispose();

			return size;
		}

		public void DrawText(string text, float x, float y)
		{
			var attributedString = new NSAttributedString(text, new CTStringAttributes() { Font = font.NativeFont, ForegroundColorFromContext = true });
			CTLine ctLine = new CTLine(attributedString);

			context.SaveState();
			context.ScaleCTM(1f, -1f);

			context.TextPosition = new CGPoint(x, -y - font.NativeFont.CapHeightMetric);
			ctLine.Draw(context);

			context.RestoreState();

			ctLine.Dispose();
			attributedString.Dispose();
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
			var attributedString = new NSAttributedString(text, new CTStringAttributes() { Font = font.NativeFont, ForegroundColorFromContext = true });
			CTLine ctLine = new CTLine(attributedString);

			CGSize size = new CGSize((float)ctLine.GetTypographicBounds(), font.NativeFont.CapHeightMetric + font.NativeFont.DescentMetric);

			if ((hAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				x += (width - (float)size.Width) / 2;
			else if ((hAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				x += width - (float)size.Width;

			if ((vAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				y += (height - (float)size.Height) / 2;
			else if ((vAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				y += height - (float)size.Height;

			context.SaveState();
			context.ScaleCTM(1f, -1f);

			context.TextPosition = new CGPoint(x, -y - font.NativeFont.CapHeightMetric);
			ctLine.Draw(context);

			context.RestoreState();

			ctLine.Dispose();
			attributedString.Dispose();
		}

		public void DrawPath(IPath path, float x, float y)
		{
			context.SaveState();
			context.TranslateCTM(x, y);
			context.AddPath(((Path)path).NativePath);
			context.DrawPath(CGPathDrawingMode.Stroke);
			context.RestoreState();
		}

		public void FillPath(IPath path, float x, float y)
		{
			context.SaveState();
			context.TranslateCTM(x, y);
			context.AddPath(((Path)path).NativePath);
			context.DrawPath(CGPathDrawingMode.Fill);
			context.RestoreState();
		}

		public void DrawImage(IImage image, float x, float y, float width, float height)
		{
			if (image != null)
			{
				CGImage cgImage = ((Image)image).NativeImage;
				if (cgImage != null)
					context.DrawImage(new CGRect(x, y, width, height), cgImage);
			}
		}

		public void Save()
		{
			context.SaveState();
		}

		public void Restore()
		{
			context.RestoreState();
		}

		public void Translate(float dx, float dy)
		{
			context.TranslateCTM(dx, dy);
		}

		public void Scale(float sx, float sy)
		{
			context.ScaleCTM(sx, sy);
		}

		public void Rotate(float angle)
		{
			context.RotateCTM(angle);
		}

		public Xamarin.Forms.Color Color
		{
			get { return color; }
			set
			{
				color = value;
				CGColor nativeColor = color.ToCGColor();
				context.SetFillColor(nativeColor); 
				context.SetStrokeColor(nativeColor); 
			}
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
