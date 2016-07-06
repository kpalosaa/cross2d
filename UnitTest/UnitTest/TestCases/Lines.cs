using System;
using Xamarin.Forms;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.Lines), typeof(Uni2D.UnitTest.LinesRenderer))]
#endif
#if __ANDROID__
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.Lines), typeof(Uni2D.UnitTest.LinesRenderer))]
#endif

namespace Uni2D.UnitTest
{
	[UnitTest(Name="Lines")]
	public class Lines : Xamarin.Forms.View
	{
	}

	public class LinesRenderer : Renderer<Lines>
	{
		protected override void Draw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawLine(50, 550, 200, 750);
			context.StrokeCapStyle = CapStyle.Flat;
			context.DrawLine(50, 600, 200, 800);
			context.StrokeCapStyle = CapStyle.Square;
			context.DrawLine(50, 650, 200, 850);
		}
	}
}
