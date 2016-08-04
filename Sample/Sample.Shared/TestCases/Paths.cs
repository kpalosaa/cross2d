using System;
using Xamarin.Forms;

namespace Cross2D.Sample
{
	[Sample(Name = "Paths")]
	public class Paths : Cross2DView
	{
		IPath triangle;
		IPath arc;

		protected override void OnCreated()
		{
			triangle = CreatePath();
			triangle.BeginFigure(0, 0);
			triangle.AddLine(150, 120);
			triangle.AddLine(0, 240);
			triangle.EndFigure(true);

			arc = CreatePath();
			arc.BeginFigure(0, 0);
			arc.AddArc(0, 0, 100, MathHelper.ConvertDegToRad(0), MathHelper.ConvertDegToRad(90));
			arc.EndFigure(true);
		}

		protected override void OnDeleted()
		{
			triangle.Dispose();
			arc.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawPath(triangle, 150, 50);

			context.StrokeWidth = 1;
			context.DrawPath(arc, 350, 50);

			context.Rotate(MathHelper.ConvertDegToRad(5));

			context.StrokeWidth = 15;
			context.DrawPath(triangle, 150, 400);
		}
	}
}
