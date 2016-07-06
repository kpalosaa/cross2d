using System;
using Xamarin.Forms;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.TestView), typeof(Uni2D.UnitTest.TestViewRenderer))]
#endif
#if __ANDROID__
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.TestView), typeof(Uni2D.UnitTest.TestViewRenderer))]
#endif

namespace Uni2D.UnitTest
{
    public class TestView : Xamarin.Forms.View
	{
    }

	public class TestViewRenderer : Renderer<TestView>
	{
		public TestViewRenderer()
		{

		}

		protected override void Draw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawRect(50, 50, 200, 200);
			context.FillRect(300, 50, 200, 200);

			context.DrawCircle(150, 400, 100);
			context.FillCircle(400, 400, 100);

			context.DrawLine(50, 550, 200, 750);
			context.StrokeCapStyle = CapStyle.Flat;
			context.DrawLine(50, 600, 200, 800);
			context.StrokeCapStyle = CapStyle.Square;
			context.DrawLine(50, 650, 200, 850);
		}
	}
}
