using System;
using Xamarin.Forms;

namespace Cross2D
{
	/// <summary>
	/// Base class for creating Xamarin Forms custom control.
	/// </summary>
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

		/// <summary>
		/// Invalidate control to repaint.
		/// </summary>
		public void Invalidate()
		{			
			renderer?.Invalidate();
		}

		/// <summary>
		/// Create path. Path can be modified until the first drawing. After that it's locked and modification is not allowed.
		/// </summary>
		/// <returns>Path</returns>
		public IPath CreatePath()
		{
			if (renderer == null)
				return null;
			
			return renderer.CreatePath();
		}

		/// <summary>
		/// Create font.
		/// </summary>
		/// <param name="size">Font size in resolution independent units.</param>
		/// <param name="fontFamily">Font family name. Default is the platform specific default font family.</param>
		/// <param name="style">Font style. Default is regular font.</param>
		/// <returns>Font</returns>
		public IFont CreateFont(int size, string fontFamily = null, FontStyle style = 0)
		{
			if (renderer == null)
				return null;

			return renderer.CreateFont(size, fontFamily, style);
		}

		/// <summary>
		/// Create named size font with platform specific default font family.
		/// </summary>
		/// <param name="namedSize">Named size. Default is platform specific default named size.</param>
		/// <param name="style">Font style. Default is regular font.</param>
		/// <returns>Font</returns>
		public IFont CreateFont(Xamarin.Forms.NamedSize namedSize = Xamarin.Forms.NamedSize.Default, FontStyle style = 0)
		{
			if (renderer == null)
				return null;

			return renderer.CreateFont(namedSize, style);
		}

		/// <summary>
		/// Create image. Supported image source is currently embedded resource.
		/// </summary>
		/// <param name="source">Image source.</param>
		/// <returns>Image</returns>
		public IImage CreateImage(Xamarin.Forms.ImageSource source)
		{
			if (renderer == null)
				return null;

			return renderer.CreateImage(source);
		}

		/// <summary>
		/// Is control created and ready for drawing.
		/// </summary>
		public bool IsCreated { get { return renderer != null; } }

		/// <summary>
		/// Called when control is ready for resource creation (fonts, images).
		/// </summary>
		protected virtual void OnCreated() { }

		/// <summary>
		/// Called when control is destroying and resources should be freed.
		/// </summary>
		protected virtual void OnDeleted() { }

		/// <summary>
		/// Called when control should be repainted.
		/// </summary>
		/// <param name="context">Drawing context.</param>
		protected abstract void OnDraw(IContext context);
	}
}
