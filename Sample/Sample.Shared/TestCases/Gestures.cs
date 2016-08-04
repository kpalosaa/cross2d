using System;
using Xamarin.Forms;

namespace Cross2D.Sample
{
	[Sample(Name = "Gestures")]
	public class Gestures : StackLayout
	{
		public Gestures()
		{
			GestureButton button = new GestureButton
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				WidthRequest = 150,
				HeightRequest = 150
			};

			Children.Add(button);
		}
	}

	public class GestureButton : Cross2DView
	{
		IFont font;

		protected override void OnParentSet()
		{
			base.OnParentSet();

			var tapGestureRecognizer = new TapGestureRecognizer();
			//tapGestureRecognizer.Command = new Command(() => { OnTapped(null, EventArgs.Empty); });
			tapGestureRecognizer.Tapped += OnTapped;
			GestureRecognizers.Add(tapGestureRecognizer);
		}

		private void OnTapped(object sender, EventArgs e)
		{
			Value += 1;
		}

		private int value = 0;
		public int Value
		{ 
			get { return value; }
			set { this.value = value; Invalidate(); }
		}

		protected override void OnCreated()
		{
			font = CreateFont(NamedSize.Large);
		}

		protected override void OnDeleted()
		{
			font.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			context.Color = Color.Navy;
			context.FillCircle(context.Width / 2, context.Height / 2, Math.Min(context.Width, context.Height) / 2);

			context.Color = Color.Green;
			context.StrokeWidth = 3;
			context.DrawCircle(context.Width / 2, context.Height / 2, Math.Min(context.Width, context.Height) / 2);

			context.SetFont(font);
			context.DrawText(Value.ToString(), 0, 0, context.Width, context.Height, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}
	}
}
