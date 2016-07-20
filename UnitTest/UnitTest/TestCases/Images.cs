using System;
using Xamarin.Forms;
using System.Reflection;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.Images), typeof(Uni2D.UnitTest.ImagesRenderer))]
#else
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.Images), typeof(Uni2D.UnitTest.ImagesRenderer))]
#endif

namespace Uni2D.UnitTest
{
	[UnitTest(Name = "Images")]
	public class Images : Xamarin.Forms.View
	{
	}

	public class ImagesRenderer : Renderer<Images>
	{
		protected override void Draw(IContext context)
		{
			IImage image;

			var assembly = typeof(ImagesRenderer).GetTypeInfo().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
			{
				System.Diagnostics.Debug.WriteLine(">>> " + res);
			}

			image = CreateImage(ImageSource.FromResource("Uni2D.testimage.jpg"));
			context.DrawImage(image, 100, 100, 200, 200 * image.Height / image.Width);

			//image = CreateImage(ImageSource.FromFile("testimage.jpg"));
			//context.DrawImage(image, 100, 400, 200, 200 * image.Height / image.Width);
		}
	}
}
