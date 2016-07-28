using System;
using Android.Views;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Cross2D.Cross2DView), typeof(Cross2D.Cross2DRenderer))]

namespace Cross2D
{
	public class Cross2DRenderer : Xamarin.Forms.Platform.Android.ViewRenderer<Cross2DView, View>, IRenderer
	{
		private NativeView nativeView;
		private Context context;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				context.Dispose();
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Cross2DView> e)
		{
			base.OnElementChanged(e);

			if (nativeView == null)
			{
				nativeView = new NativeView(this.Context);
				SetNativeControl(nativeView);

				context = new Context(nativeView);
			}

			if (e.OldElement != null)
			{
				nativeView.DrawView -= OnDrawView;
				e.OldElement.DeletedInternal();
			}

			if (e.NewElement != null)
			{
				nativeView.DrawView += OnDrawView;
				e.NewElement.CreatedInternal(this);
			}
		}

		private void OnDrawView(object sender, DrawViewEventArgs e)
		{
			context.canvas = e.Canvas;
			Element.DrawInternal(context);
		}

		public new void Invalidate()
		{
			Control.Invalidate();
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
			return new Image(Context, source);
		}
	}
}
