using System;
using Xamarin.Forms;

namespace Cross2D.UnitTest
{
	[UnitTest(Name="Lines")]
	public class Lines : Cross2DView
	{
		protected override void OnDraw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawLine(50, 50, 200, 200);
			context.StrokeCapStyle = CapStyle.Flat;
			context.DrawLine(50, 90, 200, 240);
			context.StrokeCapStyle = CapStyle.Square;
			context.DrawLine(50, 130, 200, 280);
		}
	}
}
