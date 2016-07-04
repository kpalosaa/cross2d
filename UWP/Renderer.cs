using System;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.UWP.ViewRenderer<TView, CanvasControl> where TView : Xamarin.Forms.View, IRenderer
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

		protected abstract void Draw(IContext context);
	}
}
