using System;
using Xamarin.Forms;

namespace Cross2D
{
	/// <summary>
	/// Image.
	/// </summary>
	public interface IImage : IDisposable
	{
		/// <summary>
		/// Image source. Supported image source is currently embedded resource.
		/// </summary>
		ImageSource Source { get; set; }

		/// <summary>
		/// Image size in pixels.
		/// </summary>
		Size Size { get; }

		/// <summary>
		/// Image width in pixels.
		/// </summary>
		float Width { get; }

		/// <summary>
		/// Image height in pixels.
		/// </summary>
		float Height { get; }
	}
}
