using System;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Cross2D
{
	public class Image : IImage
	{
		private ImageSource source = null;

		private Android.Content.Context context;
		private Bitmap bitmap = null;

		public Image(Android.Content.Context context, ImageSource source = null)
		{
			this.context = context;
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
				if (bitmap != null)
					bitmap.Dispose();
			}
		}

		public Size Size
		{
			get
			{
				if (bitmap == null)
				{
					return Size.Zero;
				}
				else
				{
					return new Size(bitmap.Width, bitmap.Height);
				}
			}
		}

		public float Width { get { return bitmap != null ? bitmap.Width : 0; } }
		public float Height { get { return bitmap != null ? bitmap.Height : 0; } }

		internal Bitmap NativeImage
		{
			get
			{
				return bitmap;
			}
		}

		public ImageSource Source
		{
			get { return source; }
			set
			{
				if (source == value)
					return;

				if (bitmap != null)
				{
					bitmap.Dispose();
					bitmap = null;
				}

				source = value;

				if (source == null)
					return;

				if (source is FileImageSource)
				{
					try
					{
						var filesource = source as FileImageSource;
						if (filesource != null)
						{
							var file = filesource.File;
							if (!String.IsNullOrWhiteSpace(file))
							{
								if (File.Exists(file))
									bitmap = BitmapFactory.DecodeFile(file);
								else
									bitmap = context.Resources.GetBitmap(file);
							}
						}
					}
					catch (Exception)
					{
						bitmap = null;
					}
				}
				else if (source is StreamImageSource)
				{
					try
					{
						var streamsource = source as StreamImageSource;
						if (streamsource != null && streamsource.Stream != null)
						{
							using (var stream = streamsource.Stream.Invoke(default(CancellationToken)).Result)
							{
								bitmap = BitmapFactory.DecodeStream(stream);
							}
						}
					}
					catch (Exception)
					{
						bitmap = null;
					}
				}
			}
		}
	}
}
