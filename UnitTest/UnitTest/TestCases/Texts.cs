using System;
using Xamarin.Forms;

namespace Cross2D.UnitTest
{
	[UnitTest(Name = "Texts")]
	public class Texts : Cross2DView
	{
		IFont fontSmall, fontLarge;

		protected override void OnCreated()
		{
			fontSmall = CreateFont(20);
			fontLarge = CreateFont(40);
		}

		protected override void OnDeleted()
		{
			fontSmall.Dispose();
			fontLarge.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			Size size;

			context.SetFont(fontSmall);

			size = context.MeasureText("MMggyyMM");
			context.Color = Color.Gray;
			context.FillRect(10, 10, (float)size.Width, (float)size.Height);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 10);

			context.Color = Color.Gray;
			context.FillRect(10, 50, 150, 30);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 50, 150, 30, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);

			context.Color = Color.Gray;
			context.FillRect(10, 90, 150, 40);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 90, 150, 40, Xamarin.Forms.TextAlignment.Start, Xamarin.Forms.TextAlignment.Start);

			context.Color = Color.Gray;
			context.FillRect(10, 140, 150, 40);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 140, 150, 40, Xamarin.Forms.TextAlignment.End, Xamarin.Forms.TextAlignment.End);

			context.SetFont(fontLarge);

			size = context.MeasureText("MMggyyMM");
			context.Color = Color.Gray;
			context.FillRect(10, 190, (float)size.Width, (float)size.Height);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 190);

			context.Color = Color.Gray;
			context.FillRect(10, 250, 250, 60);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 10, 250, 250, 60, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}
	}
}
