using System;
using System.Reflection;
using UIKit;
using CoreGraphics;

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

				context = new Context(nativeView, 1.0f /*(float)UIScreen.MainScreen.Scale*/);
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

				foreach (var gr in e.NewElement.GestureRecognizers)
				{
					if (gr is Xamarin.Forms.TapGestureRecognizer)
					{
						Xamarin.Forms.TapGestureRecognizer tgr = gr as Xamarin.Forms.TapGestureRecognizer;
						nativeView.AddGestureRecognizer(new UITapGestureRecognizer(g =>
						{ InvokeTapEvent(e.NewElement, tgr, Control, g); })
						{ NumberOfTapsRequired = (nuint)tgr.NumberOfTapsRequired });
					}
					else if (gr is Xamarin.Forms.PinchGestureRecognizer)
					{
						Xamarin.Forms.PinchGestureRecognizer pgr = gr as Xamarin.Forms.PinchGestureRecognizer;
						nativeView.AddGestureRecognizer(new UIPinchGestureRecognizer(g =>
						{ InvkokeEvent(pgr, "PinchUpdated", e.NewElement, CreatePinchEventArgs(Control, g)); }));
					}
				}
			}
		}

		private void InvokeTapEvent(Xamarin.Forms.View view, Xamarin.Forms.TapGestureRecognizer tgr, UIView uiView, UITapGestureRecognizer nativeTgr)
		{
			if (tgr.Command != null)
			{
				if (tgr.Command.CanExecute(tgr.CommandParameter))
					tgr.Command.Execute(tgr.CommandParameter);
			}
			else
			{
				InvkokeEvent(tgr, "Tapped", view, EventArgs.Empty);
			}
		}

		private Xamarin.Forms.PinchGestureUpdatedEventArgs CreatePinchEventArgs(UIView uiView, UIPinchGestureRecognizer pgr)
		{
			Xamarin.Forms.GestureStatus status;
			switch (pgr.State)
			{
				case UIGestureRecognizerState.Began:
					status = Xamarin.Forms.GestureStatus.Started;
					break;
				case UIGestureRecognizerState.Changed:
					status = Xamarin.Forms.GestureStatus.Running;
					break;
				case UIGestureRecognizerState.Ended:
					status = Xamarin.Forms.GestureStatus.Completed;
					break;
				case UIGestureRecognizerState.Cancelled:
					status = Xamarin.Forms.GestureStatus.Canceled;
					break;
				default:
					return null;
			}

			CGPoint location = pgr.LocationInView(uiView);

			Xamarin.Forms.PinchGestureUpdatedEventArgs args = new Xamarin.Forms.PinchGestureUpdatedEventArgs
				(
					status,
					pgr.Scale,
					new Xamarin.Forms.Point(location.X, location.Y)
				);

			return args;
		}

		private void InvkokeEvent(object obj, string eventName, object sender, EventArgs args)
		{
			FieldInfo eventField = obj.GetType().GetField(eventName, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
			if (eventField == null)
				return;

			EventHandler eventDelegate = (EventHandler)eventField.GetValue(obj);
			if (eventDelegate == null)
				return;

			foreach (var d in eventDelegate.GetInvocationList())
			{
				d.Method.Invoke(d.Target, new object[] { sender, args });
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
