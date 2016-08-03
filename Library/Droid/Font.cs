using System;
using Android.Graphics;

namespace Cross2D
{
	public class Font : IFont
	{
		private Typeface nativeTypeface;
		private float nativeSize;

		protected Font()
		{
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
				if (nativeTypeface != null)
					nativeTypeface.Dispose();
			}
		}

		public Font(string name, float size, FontStyle style = 0)
		{
			nativeTypeface = Typeface.Create(name, GetFontStyle(style));
			nativeSize = size;
		}

		public Font(float size, FontStyle style = 0)
		{
			nativeTypeface = Typeface.DefaultFromStyle(GetFontStyle(style));
			nativeSize = size;
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

		internal Typeface NativeTypeface { get { return nativeTypeface; } }
		internal float NativeSize { get { return nativeSize; } }
	}
}

