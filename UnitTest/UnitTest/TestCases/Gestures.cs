﻿using System;
using Xamarin.Forms;

namespace Cross2D.UnitTest
{
	[UnitTest(Name = "Gestures")]
	public class Gestures : Cross2DView
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
			context.FillCircle(70, 70, 50);

			context.Color = Color.Green;
			context.StrokeWidth = 3;
			context.DrawCircle(70, 70, 50);

			context.SetFont(font);
			context.DrawText(Value.ToString(), 20, 20, 100, 100, Xamarin.Forms.TextAlignment.Center, Xamarin.Forms.TextAlignment.Center);
		}
	}
}
