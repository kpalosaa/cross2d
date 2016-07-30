using System;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;

[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(Cross2D.Cross2DView), typeof(Cross2D.Cross2DRenderer))]

namespace Cross2D
{
	internal class Cross2DRenderer : Xamarin.Forms.Platform.UWP.ViewRenderer<Cross2DView, CanvasControl>, IRenderer
	{
		private CanvasControl nativeView;
		private Context context;
		private float scale;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				context.Dispose();
				nativeView.RemoveFromVisualTree();
				nativeView = null;
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.UWP.ElementChangedEventArgs<Cross2DView> e)
		{
			base.OnElementChanged(e);

			if (nativeView == null)
			{
				nativeView = new CanvasControl();
				SetNativeControl(nativeView);
			}

			if (e.OldElement != null)
			{
				nativeView.CreateResources -= OnCreateResources;
				nativeView.Draw -= OnDraw;
				e.OldElement.DeletedInternal();
			}

			if (e.NewElement != null)
			{
				nativeView.CreateResources += OnCreateResources;
				nativeView.Draw += OnDraw;
			}
		}

		private void OnCreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
		{
			if (args.Reason == CanvasCreateResourcesReason.FirstTime)
			{
				scale = nativeView.Dpi / 96.0f;
				context = new Context(Control, scale);
				Element.CreatedInternal(this);
			}
		}

		private void OnDraw(CanvasControl canvas, CanvasDrawEventArgs args)
		{
			context.ds = args.DrawingSession;
			context.ds.Units = CanvasUnits.Pixels;

			Element.DrawInternal(context);
		}

		public void Invalidate()
		{
			Control.Invalidate();
		}

		public IPath CreatePath()
		{
			return new Path(Control);
		}

		public IFont CreateFont(int size, string fontFamily = null, FontStyle style = 0)
		{
			if (fontFamily == null)
				return new Font(size * scale, style);
			else
				return new Font(fontFamily, size, style);
		}

		public IFont CreateFont(Xamarin.Forms.NamedSize namedSize = Xamarin.Forms.NamedSize.Default, FontStyle style = 0)
		{
			return new Font((float)Xamarin.Forms.Device.GetNamedSize(namedSize, typeof(Xamarin.Forms.Label)) * scale, style);
		}

		public IImage CreateImage(Xamarin.Forms.ImageSource source)
		{
			return new Image(Control, source);
		}
	}
}
