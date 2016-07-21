using System;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;

namespace Uni2D
{
	public abstract class Renderer<TView> : Xamarin.Forms.Platform.UWP.ViewRenderer<TView, CanvasControl> where TView : Xamarin.Forms.View
	{
		private CanvasControl view;
		private Context context;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				context.Dispose();
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.UWP.ElementChangedEventArgs<TView> e)
		{
			base.OnElementChanged(e);

			if (view == null)
			{
				view = new CanvasControl();
				SetNativeControl(view);
			}

			if (e.OldElement != null)
			{
				e.OldElement.SizeChanged -= OnSizeChanged;
				view.CreateResources -= OnCreateResources;
				view.Draw -= OnDraw;
			}

			if (e.NewElement != null)
			{
				e.NewElement.SizeChanged += OnSizeChanged;
				view.CreateResources += OnCreateResources;
				view.Draw += OnDraw;
			}
		}

		private void OnCreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
		{
			if (args.Reason == CanvasCreateResourcesReason.FirstTime)
			{
				context = new Context(Control);
				Created();
			}
		}

		private void OnSizeChanged(object sender, EventArgs e)
		{
			SizeChanged((float)Control.Width, (float)Control.Height);
		}

		private void OnDraw(CanvasControl canvas, CanvasDrawEventArgs args)
		{
			context.ds = args.DrawingSession;

			Draw(context);
		}

		public void Invalidate()
		{
			Control.Invalidate();
		}

		public IPath CreatePath()
		{
			return new Path(Control);
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

		protected virtual void Created() { }
		protected new virtual void SizeChanged(float width, float height) { }
		protected abstract void Draw(IContext context);
	}
}
