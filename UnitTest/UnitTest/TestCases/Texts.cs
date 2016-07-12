using System;
using Xamarin.Forms;
using TextAlignment = Xamarin.Forms.TextAlignment;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.Texts), typeof(Uni2D.UnitTest.TextRenderer))]
#else
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.Texts), typeof(Uni2D.UnitTest.TextRenderer))]
#endif

namespace Uni2D.UnitTest
{
	[UnitTest(Name = "Texts")]
	public class Texts : Xamarin.Forms.View
	{
	}

	public class TextRenderer : Renderer<Texts>
	{
		protected override void Draw(IContext context)
		{
			Size size;
			IFont font;

			font = CreateFont(20);
			context.SetFont(font);

			size = context.MeasureText("MMggyyMM");
			context.Color = Color.Gray;
			context.FillRect(10, 10, (float)size.Width, (float)size.Height);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 10);

			context.Color = Color.Gray;
			context.FillRect(10, 50, 150, 30);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 50, 150, 30, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);

			context.Color = Color.Gray;
			context.FillRect(10, 90, 150, 40);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 90, 150, 40, Xamarin.Forms.TextAlignment.Start, Xamarin.Forms.TextAlignment.Start);

			context.Color = Color.Gray;
			context.FillRect(10, 140, 150, 40);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 140, 150, 40, Xamarin.Forms.TextAlignment.End, Xamarin.Forms.TextAlignment.End);
		
			font = CreateFont(40);
			context.SetFont(font);

			size = context.MeasureText("MMggyyMM");
			context.Color = Color.Gray;
			context.FillRect(10, 190, (float)size.Width, (float)size.Height);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 190);

			context.Color = Color.Gray;
			context.FillRect(10, 250, 250, 60);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 250, 250, 60, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}
	}
}

