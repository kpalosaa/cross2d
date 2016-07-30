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

			float y = 20;

			size = context.MeasureText("MMggyyMM");
			context.Color = Color.Gray;
			context.FillRect(20, y, (float)size.Width, (float)size.Height);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 20, y);
			y += (float)size.Height + 10;
			context.Color = Color.Gray;
			context.FillRect(20, y, 350, 60);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 20, y, 350, 60, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
			y += 60 + 10;
			context.Color = Color.Gray;
			context.FillRect(20, y, 350, 60);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 20, y, 350, 60, Xamarin.Forms.TextAlignment.Start, Xamarin.Forms.TextAlignment.Start);
			y += 60 + 10;
			context.Color = Color.Gray;
			context.FillRect(20, y, 350, 60);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 20, y, 350, 60, Xamarin.Forms.TextAlignment.End, Xamarin.Forms.TextAlignment.End);

			context.SetFont(fontLarge);

			y += 60 + 10;
			size = context.MeasureText("MMggyyMM");
			context.Color = Color.Gray;
			context.FillRect(20, y, (float)size.Width, (float)size.Height);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 20, y);
			y += (float)size.Height + 10;
			context.Color = Color.Gray;
			context.FillRect(20, y, 450, 90);
			context.Color = Color.Green;
			context.DrawText("MMggyyMM", 20, y, 450, 90, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}
	}
}
