using System;
using Xamarin.Forms;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.Circles), typeof(Uni2D.UnitTest.CirclesRenderer))]
#endif
#if __ANDROID__
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.Circles), typeof(Uni2D.UnitTest.CirclesRenderer))]
#endif

namespace Uni2D.UnitTest
{
	[UnitTest(Name = "Circles")]
	public class Circles : Xamarin.Forms.View
	{
	}

	public class CirclesRenderer : Renderer<Circles>
	{
		protected override void Draw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawCircle(150, 400, 100);
			context.FillCircle(400, 400, 100);
		}
	}
}
