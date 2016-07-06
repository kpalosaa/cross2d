using System;
using CoreGraphics;
using UIKit;

namespace Uni2D
{
	public class View : UIView
	{
		public event DrawViewEventHandler DrawView;

		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			var width = Bounds.Size.Width;
			var height = Bounds.Size.Height;

			using (var context = UIGraphics.GetCurrentContext())
			{
				DrawView?.Invoke(this, new DrawViewEventArgs() { Context = context, Rect = rect });
			}
		}
	}

	public delegate void DrawViewEventHandler(object sender, DrawViewEventArgs e);

	public class DrawViewEventArgs : EventArgs
	{
		public CGContext Context { get; set; }
		public CGRect Rect { get; set; }
	}
}
