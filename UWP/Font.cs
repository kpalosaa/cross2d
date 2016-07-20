using System;
using Microsoft.Graphics.Canvas.Text;

namespace Uni2D
{
	public class Font : IFont
	{
		private CanvasTextFormat textFormat = new CanvasTextFormat();

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
				textFormat.Dispose();
			}
		}

		public Font(string name, int size, FontStyle style = 0)
		{
			SetFontStyle(style);

			textFormat.FontFamily = name;
			textFormat.FontSize = size;
		}

		public Font(int size, FontStyle style = 0)
		{
			SetFontStyle(style);

			textFormat.FontSize = size;
		}

		public Font(Xamarin.Forms.NamedSize namedSize, FontStyle style = 0)
		{
			SetFontStyle(style);

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

		internal CanvasTextFormat NativeTextFormat { get { return textFormat; } }
	}
}
