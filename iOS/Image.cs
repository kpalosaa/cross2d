using System;
using System.IO;
using System.Threading;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Foundation;

namespace Cross2D
{
	public class Image : IImage
	{
		private Xamarin.Forms.ImageSource source = null;

		private UIImage uiImage = null;

		public Image(Xamarin.Forms.ImageSource source = null)
		{
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
				if (uiImage != null)
					uiImage.Dispose();
			}
		}

		public Size Size
		{ 
			get 
			{
				if (uiImage == null)
				{
					return Size.Zero;
				}
				else
				{
					CGSize size = uiImage.Size;
					return new Size(size.Width, size.Height);
				}
			}
		}

		public float Width { get { return uiImage != null ? (float)uiImage.Size.Width : 0; } }
		public float Height { get { return uiImage != null ? (float)uiImage.Size.Height : 0; } }

		internal CGImage NativeImage
		{
			get
			{
				if (uiImage == null)
					return null;
				
				return uiImage.CGImage;
			}
		}

		public Xamarin.Forms.ImageSource Source
		{
			get { return source; }
			set
			{
				if (source == value)
					return;
				
				if (uiImage != null)
				{
					uiImage.Dispose();
					uiImage = null;
				}

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
								uiImage = File.Exists(file) ? new UIImage(file) : UIImage.FromBundle(file);
						}
					}
					catch (Exception)
					{
						uiImage = null;
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
								uiImage = UIImage.LoadFromData(NSData.FromStream(stream), 1.0f);
							}
						}
					}
					catch (Exception)
					{
						uiImage = null;
					}
				}
			}
		}
	}
}
