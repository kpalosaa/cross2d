using System;
using Xamarin.Forms;

namespace Cross2D.Sample
{
	[Sample(Name = "Buttons")]
	public class Buttons : StackLayout
	{
		public Buttons()
		{
			HalfRoundedButton button = new HalfRoundedButton
			{
				Text = "Half Rounded Button",
				CornerRadius = 30,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				WidthRequest = 200,
				HeightRequest = 100
			};

			Children.Add(button);
		}
	}

	public class HalfRoundedButton : Cross2DView
	{
		IFont font;
		IPath path;

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
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			if (path != null)
			{
				path.Dispose();
				path = null;
			}

			base.OnSizeAllocated(width, height);
		}

		protected override void OnCreated()
		{
			font = CreateFont(NamedSize.Default);
		}

		protected override void OnDeleted()
		{
			font.Dispose();
		}

		protected override void OnDraw(IContext context)
		{
			if (path == null)
			{
				path = CreatePath();

				path.BeginFigure(1, 1 + cornerRadius);
				path.AddArc(1 + cornerRadius, 1 + cornerRadius, cornerRadius, MathHelper.ConvertDegToRad(180), MathHelper.ConvertDegToRad(270));
				path.AddLine(context.Width - 2, 1);
				path.AddLine(context.Width - 2, context.Height - 2 - cornerRadius);
				path.AddArc(context.Width - 2 - cornerRadius, context.Height - 2 - cornerRadius, cornerRadius, MathHelper.ConvertDegToRad(0), MathHelper.ConvertDegToRad(90));
				path.AddLine(1, context.Height - 2);
				path.AddLine(1, 1 + cornerRadius);
				path.EndFigure(false);
			}

			context.Color = Color.Green;
			context.StrokeWidth = 3;
			context.DrawPath(path, 0, 0);

			context.SetFont(font);
			context.DrawText(text, 0, 0, context.Width, context.Height, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}

		private string text = "";
		public string Text
		{
			get { return text; }
			set
			{
				if (text != value)
				{
					text = value;
					Invalidate();
				}
			}
		}

		private float cornerRadius = 5.0f;
		public float CornerRadius
		{
			get { return cornerRadius; }
			set
			{
				if (cornerRadius != value)
				{
					cornerRadius = value;

					if (path != null)
					{
						path.Dispose();
						path = null;
					}

					Invalidate();
				}
			}
		}
	}
}
