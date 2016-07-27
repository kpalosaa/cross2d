using System;
using Xamarin.Forms;

namespace Cross2D
{
	public abstract class Cross2DView : Xamarin.Forms.View
	{
		private IRenderer renderer;

		internal void DrawInternal(IContext context)
		{
			OnDraw(context);
		}

		internal void CreatedInternal(IRenderer renderer)
		{
			this.renderer = renderer;
			OnCreated();
		}

		internal void DeletedInternal()
		{
			renderer = null;
			OnDeleted();
		}

		public void Invalidate()
		{
			renderer.Invalidate();
		}

		public IPath CreatePath()
		{
			return renderer.CreatePath();
		}

		public IFont CreateFont(int size, string fontFamily = null, FontStyle style = 0)
		{
			return renderer.CreateFont(size, fontFamily, style);
		}

		public IFont CreateFont(Xamarin.Forms.NamedSize namedSize = Xamarin.Forms.NamedSize.Default, FontStyle style = 0)
		{
			return renderer.CreateFont(namedSize, style);
		}

		public IImage CreateImage(Xamarin.Forms.ImageSource source)
		{
			return renderer.CreateImage(source);
		}

		protected virtual void OnCreated() { }
		protected virtual void OnDeleted() { }
		protected abstract void OnDraw(IContext context);
	}
}
