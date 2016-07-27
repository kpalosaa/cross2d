using System;
using Xamarin.Forms;

namespace Cross2D.UnitTest
{
	[UnitTest(Name = "Paths")]
	public class Paths : Cross2DView
	{
		IPath path;

		protected override void OnCreated()
		{
			path = CreatePath();
			path.BeginFigure(0, 0);
			path.AddLine(150, 120);
			path.AddLine(0, 240);
			path.EndFigure(true);
		}

		protected override void OnDeleted()
		{
			path.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawPath(path, 150, 50);

			context.Rotate(MathHelper.ConvertDegToRad(5));

			context.DrawPath(path, 150, 400);
		}
	}
}
