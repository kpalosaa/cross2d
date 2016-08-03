using System;
using CoreText;
using UIKit;

namespace Cross2D
{
	public class Font : IFont
	{
		private CTFont nativeFont;

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
				nativeFont.Dispose();
			}
		}

		public Font(string name, int size, FontStyle style = 0)
		{
			CTFontDescriptorAttributes fda = new CTFontDescriptorAttributes()
			{
				FamilyName = name,
				Size = size
			};

			if ((style & FontStyle.Bold) != 0)
				fda.StyleName = "Bold";
			else if ((style & FontStyle.Italic) != 0)
				fda.StyleName = "Italic";
			
			CTFontDescriptor fd = new CTFontDescriptor(fda);
			nativeFont = new CTFont(fd, 0);
			fd.Dispose();
		}

		public Font(int size, FontStyle style = 0)
		{
			UIFont uiFont = UIFont.SystemFontOfSize(size);
			CTFontDescriptorAttributes fda = new CTFontDescriptorAttributes()
			{
				FamilyName = uiFont.FamilyName,
				Size = (float)uiFont.PointSize
			};

			uiFont.Dispose();

			if ((style & FontStyle.Bold) != 0)
				fda.StyleName = "Bold";
			else if ((style & FontStyle.Italic) != 0)
				fda.StyleName = "Italic";
			
			CTFontDescriptor fd = new CTFontDescriptor(fda);
			nativeFont = new CTFont(fd, 0);
			fd.Dispose();
		}

		public Font(Xamarin.Forms.NamedSize namedSize, FontStyle style = 0)
		{
			UIFont uiFont = UIFont.SystemFontOfSize((float)Xamarin.Forms.Device.GetNamedSize(namedSize, typeof(Xamarin.Forms.Label)));
			CTFontDescriptorAttributes fda = new CTFontDescriptorAttributes()
			{
				FamilyName = uiFont.FamilyName,
				Size = (float)uiFont.PointSize
			};

			uiFont.Dispose();

			if ((style & FontStyle.Bold) != 0)
				fda.StyleName = "Bold";
			else if ((style & FontStyle.Italic) != 0)
				fda.StyleName = "Italic";

			CTFontDescriptor fd = new CTFontDescriptor(fda);
			nativeFont = new CTFont(fd, 0);
			fd.Dispose();
		}

		internal CTFont NativeFont { get { return nativeFont; } }
	}
}
