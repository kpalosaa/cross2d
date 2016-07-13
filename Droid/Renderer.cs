using System;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.Android.ViewRenderer<TView, View> where TView: Xamarin.Forms.View
	{
		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<TView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				View view = new View(this.Context);
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
			using (Context context = new Context(e.Canvas, e.Rect))
			{
				Draw(context);
			}
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

		protected abstract void Draw(IContext context);
	}
}
