using System;
using Android.Graphics;

namespace Cross2D
{
	public class NativeView : Android.Views.View
	{
		public event DrawViewEventHandler DrawView;

		public NativeView(Android.Content.Context context)
			: base(context)
		{
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			DrawView?.Invoke(this, new DrawViewEventArgs() { Canvas = canvas });
		}
	}

	public delegate void DrawViewEventHandler(object sender, DrawViewEventArgs e);

	public class DrawViewEventArgs : EventArgs
	{
		public Canvas Canvas { get; set; }
		public Rect Rect { get; set; }
	}
}
