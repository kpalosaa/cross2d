using System;
using Xamarin.Forms;

namespace Cross2D.Sample
{
	[Sample(Name = "Circles")]
	public class Circles : Cross2DView
	{
		protected override void OnDraw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 15;
			context.StrokeCapStyle = CapStyle.Round;

			context.DrawCircle(70, 70, 50);
			context.FillCircle(70, 190, 50);
		}
	}
}
