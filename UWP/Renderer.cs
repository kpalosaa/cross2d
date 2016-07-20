using System;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.UWP.ViewRenderer<TView, CanvasControl> where TView : Xamarin.Forms.View
	{
		protected override void OnElementChanged(Xamarin.Forms.Platform.UWP.ElementChangedEventArgs<TView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				CanvasControl view = new CanvasControl();
				SetNativeControl(view);
			}

			if (e.OldElement != null)
			{
				Control.Draw -= OnDraw;
			}

			if (e.NewElement != null)
			{
				Control.Draw += OnDraw;
			}
		}

		private void OnDraw(CanvasControl canvas, CanvasDrawEventArgs args)
		{
			Context context = new Context(Control, args.DrawingSession);

			Draw(context);
		}

		public IPath CreatePath()
		{
			return new Path();
		}

		public IFont CreateFont(string name, int size, FontStyle style = 0)
		{
			return new Font(name, size, style);
		}

		public IFont CreateFont(int size, FontStyle style = 0)
		{
			return new Font(size, style);
		}

		public IFont CreateFont(Xamarin.Forms.NamedSize namedSize, FontStyle style = 0)
		{
			return new Font(namedSize, style);
		}

		public IImage CreateImage(Xamarin.Forms.ImageSource source)
		{
			return new Image(Control, source);
		}

		protected abstract void Draw(IContext context);
	}
}
