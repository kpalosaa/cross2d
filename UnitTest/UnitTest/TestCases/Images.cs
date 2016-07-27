using System;
using Xamarin.Forms;
using System.Reflection;

namespace Cross2D.UnitTest
{
	[UnitTest(Name = "Images")]
	public class Images : Cross2DView
	{
		IImage image;

		protected override void OnCreated()
		{
			image = CreateImage(ImageSource.FromResource("Cross2D.testimage.jpg"));
		}

		protected override void OnDeleted()
		{
			image.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			var assembly = typeof(Images).GetTypeInfo().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
			{
				System.Diagnostics.Debug.WriteLine("Resource: " + res);
			}

			context.DrawImage(image, 100, 100, 200, 200 * image.Height / image.Width);
		}
	}
}
