using System;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Cross2D.Cross2DView), typeof(Cross2D.Cross2DRenderer))]

namespace Cross2D
{
	public class Cross2DRenderer : Xamarin.Forms.Platform.iOS.ViewRenderer<Cross2DView, NativeView>, IRenderer
	{
		private NativeView nativeView;
		private Context context;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				Element.DeletedInternal();
				context.Dispose();
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Cross2DView> e)
		{
			base.OnElementChanged(e);

			if (nativeView == null)
			{
				nativeView = new NativeView();
				SetNativeControl(nativeView);

				context = new Context(nativeView);
			}

			if (e.OldElement != null)
			{
				nativeView.DrawView -= OnDrawView;
			}

			if (e.NewElement != null)
			{
				nativeView.DrawView += OnDrawView;
				e.NewElement.CreatedInternal(this);
			}
		}
		
		private void OnDrawView(object sender, DrawViewEventArgs e)
		{
			context.context = e.Context;
			context.rect = e.Rect;
			context.Width = (float)Control.Bounds.Width;
			context.Height = (float)Control.Bounds.Height;
			Element.DrawInternal(context);
		}

		public void Invalidate()
		{
			Control.SetNeedsDisplay();
		}

		public IPath CreatePath()
		{
			return new Path();
		}

		public IFont CreateFont(int size, string fontFamily = null, FontStyle style = 0)
		{
			if (fontFamily == null)
				return new Font(size, style);
			else
				return new Font(fontFamily, size, style);
		}

		public IFont CreateFont(Xamarin.Forms.NamedSize namedSize = Xamarin.Forms.NamedSize.Default, FontStyle style = 0)
		{
			return new Font(namedSize, style);
		}

		public IImage CreateImage(Xamarin.Forms.ImageSource source)
		{
			return new Image(source);
		}
	}
}
