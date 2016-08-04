using System;
using Xamarin.Forms;
using System.Reflection;

namespace Cross2D.Sample
{
	[Sample(Name = "Images")]
	public class Images : Cross2DView
	{
		IImage image;
		IImage imageSmall;

		protected override void OnCreated()
		{
			image = CreateImage(ImageSource.FromResource("Cross2D.testimage.jpg"));
			imageSmall = CreateImage(ImageSource.FromResource("Cross2D.testimagesmall.jpg"));
		}

		protected override void OnDeleted()
		{
			image.Dispose();
			imageSmall.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			var assembly = typeof(Images).GetTypeInfo().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
			{
				System.Diagnostics.Debug.WriteLine("Resource: " + res);
			}

			context.Color = Color.Green;

			float x = 20;
			float y = 20;

			context.DrawImage(imageSmall, x, y, DrawingUnit.Dip);
			y += context.PixelToDip(imageSmall.Height);
			y += 20;
			context.DrawImage(imageSmall, x, y, DrawingUnit.Pixel);
			y += imageSmall.Height;
			y += 20;
			context.DrawRect(x, y, 200, 200);
			context.DrawImage(image, x + 2, y + 2, 200 - 4, 200 - 4, Aspect.Fill);
			x += 200 + 20;
			context.DrawRect(x, y, 200, 200);
			context.DrawImage(image, x + 2, y + 2, 200 - 4, 200 - 4, Aspect.AspectFit);
			x += 200 + 20;
			context.DrawRect(x, y, 200, 200);
			context.DrawImage(image, x + 2, y + 2, 200 - 4, 200 - 4, Aspect.AspectFill);
		}
	}
}
