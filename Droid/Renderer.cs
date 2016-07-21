using System;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.Android.ViewRenderer<TView, View> where TView: Xamarin.Forms.View
	{
		private Uni2D.View view;
		private Uni2D.Context context;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				context.Dispose();
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<TView> e)
		{
			base.OnElementChanged(e);

			if (view == null)
			{
				view = new Uni2D.View(this.Context);
				SetNativeControl(view);

				context = new Uni2D.Context(view);
				Created();
			}

			if (e.OldElement != null)
			{
				e.OldElement.SizeChanged -= OnSizeChanged;
				view.DrawView -= OnDrawView;
			}

			if (e.NewElement != null)
			{
				e.NewElement.SizeChanged += OnSizeChanged;
				view.DrawView += OnDrawView;
			}
		}

		private void OnSizeChanged(object sender, EventArgs e)
		{
			SizeChanged((float)Control.Width, (float)Control.Height);
		}

		private void OnDrawView(object sender, DrawViewEventArgs e)
		{
			context.canvas = e.Canvas;
			Draw(context);
		}

		public new void Invalidate()
		{
			Control.Invalidate();
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
			return new Image(Context, source);
		}

		protected virtual void Created() { }
		protected virtual void SizeChanged(float width, float height) { }
		protected abstract void Draw(IContext context);
	}
}
