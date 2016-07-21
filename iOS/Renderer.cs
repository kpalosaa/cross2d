using System;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.iOS.ViewRenderer<TView, View> where TView : Xamarin.Forms.View
	{
		private View view;
		private Context context;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				context.Dispose();
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<TView> e)
		{
			base.OnElementChanged(e);

			if (view == null)
			{
				view = new View();
				SetNativeControl(view);

				context = new Context(view);
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
			SizeChanged((float)Control.Bounds.Width, (float)Control.Bounds.Height);
		}

		private void OnDrawView(object sender, DrawViewEventArgs e)
		{
			context.context = e.Context;
			context.rect = e.Rect;
			context.Width = (float)Control.Bounds.Width;
			context.Height = (float)Control.Bounds.Height;
			Draw(context);
		}

		public void Invalidate()
		{
			Control.SetNeedsDisplay();
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
			return new Image(source);
		}

		protected virtual void Created() { }
		protected virtual void SizeChanged(float width, float height) { }
		protected abstract void Draw(IContext context);
	}
}
