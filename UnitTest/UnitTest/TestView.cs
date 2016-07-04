using System;
using Xamarin.Forms;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.TestView), typeof(Uni2D.UnitTest.TestViewRenderer))]
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
			context.Color = Color.Black;

			context.DrawCircle(10, 10, 10);
		}
	}
}
