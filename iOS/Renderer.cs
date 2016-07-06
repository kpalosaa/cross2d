using System;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.iOS.ViewRenderer<TView, View> where TView : Xamarin.Forms.View
	{
		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<TView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				View view = new View();
				SetNativeControl(view);
			}

			if (e.OldElement != null)
			{
				Control.DrawView -= OnDrawView;
			}

			if (e.NewElement != null)
			{
				Control.DrawView += OnDrawView;
			}
		}

		private void OnDrawView(object sender, DrawViewEventArgs e)
		{
			Context context = new Context(e.Context, e.Rect);
			context.Width = (float)Element.Width;
			context.Height = (float)Element.Height;

			Draw(context);
		}

		protected abstract void Draw(IContext context);
	}
}
