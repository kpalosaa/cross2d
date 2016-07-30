using System;
using Android.Views;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Cross2D.Cross2DView), typeof(Cross2D.Cross2DRenderer))]

namespace Cross2D
{
	public class Cross2DRenderer : Xamarin.Forms.Platform.Android.ViewRenderer<Cross2DView, View>, IRenderer
	{
		private NativeView nativeView;
		private Context context;
		private float density;

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

			density = Resources.DisplayMetrics.Density;

			if (nativeView == null)
			{
				nativeView = new NativeView(this.Context);
				SetNativeControl(nativeView);

				context = new Context(nativeView, density);
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
			context.Width = e.Rect.Width();
			context.Height = e.Rect.Height();
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
				return new Font(size * density, style);
			else
				return new Font(fontFamily, size * density, style);
		}

		public IFont CreateFont(Xamarin.Forms.NamedSize namedSize = Xamarin.Forms.NamedSize.Default, FontStyle style = 0)
		{
			return new Font((float)Xamarin.Forms.Device.GetNamedSize(namedSize, typeof(Xamarin.Forms.Label)) * density, style);
		}

		public IImage CreateImage(Xamarin.Forms.ImageSource source)
		{
			return new Image(Context, source);
		}
	}
}
