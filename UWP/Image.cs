using System;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Microsoft.Graphics.Canvas;
using Xamarin.Forms.Platform.UWP;

namespace Uni2D
{
	public class Image : IImage
	{
		private ICanvasResourceCreator resourceCreator;
		private Xamarin.Forms.ImageSource source = null;

		private CanvasBitmap bitmap;

		public Image(ICanvasResourceCreator resourceCreator, Xamarin.Forms.ImageSource source = null)
		{
			this.resourceCreator = resourceCreator;
			if (source != null)
				this.Source = source;
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

		public Size Size
		{
			get
			{
				if (bitmap != null)
					return new Size(bitmap.Size.Width, bitmap.Size.Height);
				else
					return Size.Zero;
			}
		}

		public float Width { get { return bitmap != null ? (float)bitmap.Size.Width : 0; } }
		public float Height { get { return bitmap != null ? (float)bitmap.Size.Height : 0; } }

		public Xamarin.Forms.ImageSource Source
		{
			get { return source; }
			set
			{
				if (source == value)
					return;


				source = value;

				if (source == null)
					return;

				if (source is Xamarin.Forms.FileImageSource)
				{
					try
					{
						var filesource = source as FileImageSource;
						if (filesource != null)
						{
							var file = filesource.File;
							if (!string.IsNullOrEmpty(file))
								bitmap = CanvasBitmap.LoadAsync(resourceCreator, new Uri("ms-appx:///" + file)).AsTask().Result;
						}
					}
					catch (Exception)
					{

					}
				}
				else if (source is Xamarin.Forms.StreamImageSource)
				{
					try
					{
						var streamsource = source as StreamImageSource;
						if (streamsource != null && streamsource.Stream != null)
						{
							using (var stream = streamsource.Stream.Invoke(default(CancellationToken)).Result)
							{
								bitmap = CanvasBitmap.LoadAsync(resourceCreator, stream.AsRandomAccessStream()).AsTask().Result;
							}
						}
					}
					catch (Exception)
					{

					}
				}
			}
		}

		internal CanvasBitmap NativeBitmap { get { return bitmap; } }
	}
}
