using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Cross2D
{
	public sealed class Context : IContext
	{
		private NativeView view;
		private float density;
		private float densityInv;

		internal Canvas canvas;

		private Paint paintStroke;
		private Paint paintFill;

		internal Context(NativeView view, float density)
		{
			this.view = view;
			this.density = density;
			this.densityInv = 1 / density;

			paintStroke = new Paint();
			paintStroke.SetStyle(Paint.Style.Stroke);

			paintFill = new Paint();
			paintFill.SetStyle(Paint.Style.Fill);
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
				paintStroke.Dispose();
				paintFill.Dispose();
			}
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

		public void SetFont(IFont font)
		{
			if (font == null || ((Font)font).NativeTypeface == null)
				return;
			
			paintFill.SetTypeface(((Font)font).NativeTypeface);
			paintFill.TextSize = ((Font)font).NativeSize;
		}

		public Xamarin.Forms.Size MeasureText(string text)
		{
			Paint.FontMetrics fm = paintFill.GetFontMetrics();

			return new Xamarin.Forms.Size(paintFill.MeasureText(text), fm.Bottom - fm.Ascent);
		}

		public void DrawText(string text, float x, float y)
		{
			canvas.DrawText(text, x, y - paintFill.GetFontMetrics().Ascent, paintFill);
		}

		public void DrawText(string text, float x, float y, float width, float height, Xamarin.Forms.TextAlignment hAlignment, Xamarin.Forms.TextAlignment vAlignment)
		{
			Paint.FontMetrics fm = paintFill.GetFontMetrics();

			float textWidth = paintFill.MeasureText(text);
			float textHeight = fm.Descent - fm.Ascent;

			if ((hAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				x += (width - textWidth) / 2;
			else if ((hAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				x += width - textWidth;

			if ((vAlignment & Xamarin.Forms.TextAlignment.Center) != 0)
				y += (height - textHeight) / 2;
			else if ((vAlignment & Xamarin.Forms.TextAlignment.End) != 0)
				y += height - textHeight;

			canvas.DrawText(text, x, y - fm.Ascent, paintFill);
		}

		public void DrawPath(IPath path, float x, float y)
		{
			if (path == null || ((Path)path).NativePath == null)
				return;

			canvas.Save();
			canvas.Translate(x, y);
			canvas.DrawPath(((Path)path).NativePath, paintStroke);
			canvas.Restore();
		}

		public void FillPath(IPath path, float x, float y)
		{
			if (path == null || ((Path)path).NativePath == null)
				return;

			canvas.Save();
			canvas.Translate(x, y);
			canvas.DrawPath(((Path)path).NativePath, paintFill);
			canvas.Restore();
		}

		public void DrawImage(IImage image, float x, float y, DrawingUnit unit = DrawingUnit.Dip)
		{
			float width = image.Width;
			float height = image.Height;

			if (unit == DrawingUnit.Dip)
			{
				width *= density;
				height *= density;
			}

			DrawImage(image, x, y, width, height, Xamarin.Forms.Aspect.Fill);
		}

		public void DrawImage(IImage image, float x, float y, float width, float height, Xamarin.Forms.Aspect aspect)
		{
			if (image != null)
			{
				Bitmap bitmap = ((Image)image).NativeImage;
				if (bitmap != null)
				{
					if (aspect == Xamarin.Forms.Aspect.Fill)
					{
						using (RectF rect = new RectF(x, y, x + width - 1, y + height - 1))
							canvas.DrawBitmap(bitmap, null, rect, null);
					}
					else
					{
						Xamarin.Forms.Size bounding = new Xamarin.Forms.Size(width, height);

						if (aspect == Xamarin.Forms.Aspect.AspectFill)
						{
							Xamarin.Forms.Rectangle imageBox = MathHelper.Fill(new Xamarin.Forms.Rectangle(Xamarin.Forms.Point.Zero, image.Size), bounding);
							using (RectF rectB = new RectF(x, y, x + width - 1, y + height - 1))
							using (Rect rectI = new Rect((int)imageBox.X, (int)imageBox.Y, (int)imageBox.X + (int)imageBox.Width - 1, (int)imageBox.Y + (int)imageBox.Height - 1))
								canvas.DrawBitmap(bitmap, rectI, rectB, null);
						}
						else
						{
							Xamarin.Forms.Rectangle boundingBox = MathHelper.Fit(image.Size, new Xamarin.Forms.Rectangle(x, y, width, height));
							using (RectF rect = new RectF((float)boundingBox.X, (float)boundingBox.Y, (float)boundingBox.X + (float)boundingBox.Width - 1, (float)boundingBox.Y + (float)boundingBox.Height - 1))
								canvas.DrawBitmap(bitmap, null, rect, null);
						}
					}
				}
			}
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

		public float DipToPixel(float dip)
		{
			return dip * densityInv;
		}

		public float PixelToDip(float pixel)
		{
			return pixel * density;
		}

		public Xamarin.Forms.Size DipToPixel(Xamarin.Forms.Size dip)
		{
			return new Xamarin.Forms.Size(dip.Width * densityInv, dip.Height * densityInv);
		}

		public Xamarin.Forms.Size PixelToDip(Xamarin.Forms.Size pixel)
		{
			return new Xamarin.Forms.Size(pixel.Width * density, pixel.Height * density);
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

		public float Width { get; internal set; }
		public float Height { get; internal set; }

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
