using System;
using Xamarin.Forms;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.Paths), typeof(Uni2D.UnitTest.PathRenderer))]
#else
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.Paths), typeof(Uni2D.UnitTest.PathRenderer))]
#endif

namespace Uni2D.UnitTest
{
	[UnitTest(Name = "Paths")]
	public class Paths : Xamarin.Forms.View
	{
	}

	public class PathRenderer : Renderer<Paths>
	{
		protected override void Draw(IContext context)
		{
			IPath path = CreatePath();
			path.BeginFigure(0, 0);
			path.AddLine(150, 120);
			path.AddLine(0, 240);
			path.EndFigure(true);

			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawPath(path, 150, 50);

			context.Rotate(MathHelper.ConvertDegToRad(5));

			context.DrawPath(path, 150, 400);
		}
	}
}
