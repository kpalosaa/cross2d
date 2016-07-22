using System;
using Xamarin.Forms;

#if WINDOWS_UWP
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Uni2D.UnitTest.Gestures), typeof(Uni2D.UnitTest.GesturesRenderer))]
#else
[assembly: Xamarin.Forms.ExportRenderer(typeof(Uni2D.UnitTest.Gestures), typeof(Uni2D.UnitTest.GesturesRenderer))]
#endif

namespace Uni2D.UnitTest
{
	[UnitTest(Name = "Gestures")]
	public class Gestures : Xamarin.Forms.View
	{
		protected override void OnParentSet()
		{
			base.OnParentSet();

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += OnTapped;
			GestureRecognizers.Add(tapGestureRecognizer);
		}

		private void OnTapped(object sender, EventArgs e)
		{
			Value += 1;
		}

		public int Value { get; set; } = 0;
	}

	public class GesturesRenderer : Renderer<Gestures>
	{
		int value;
		IFont font;

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case "Value":
					value = Element.Value;
					break;
			}
		}
	
		protected override void Created()
		{
			font = CreateFont(NamedSize.Large);
		}

		protected override void Draw(IContext context)
		{
			context.Color = Color.Green;
			context.StrokeWidth = 3;
			context.DrawCircle(70, 70, 50);

			context.SetFont(font);
			context.DrawText(value.ToString(), 20, 20, 100, 100, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}
	}
}
