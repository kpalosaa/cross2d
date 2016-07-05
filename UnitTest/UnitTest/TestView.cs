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
			context.StrokeStyle = StrokeStyle.Stroke;
			context.StrokeWidth = 5;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawCircle(context.Width / 2, context.Height / 2, Math.Min(context.Width / 2, context.Height / 2));

			context.DrawLine(0, context.Height - 10, context.Width, context.Height - 10);
		}
	}
}
