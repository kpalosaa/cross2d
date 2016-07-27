using System;

namespace Cross2D
{
	public interface IRenderer
	{
		void Invalidate();
		IPath CreatePath();
		IFont CreateFont(int size, string fontFamily = null, FontStyle style = 0);
		IFont CreateFont(Xamarin.Forms.NamedSize namedSize = Xamarin.Forms.NamedSize.Default, FontStyle style = 0);
		IImage CreateImage(Xamarin.Forms.ImageSource source);
	}
}
