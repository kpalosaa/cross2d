using System;

namespace Cross2D
{
	/// <summary>
	/// Line cap style.
	/// </summary>
	public enum CapStyle
	{
		/// <summary>
		/// Flat caps.
		/// </summary>
		Flat,
		/// <summary>
		/// Square caps.
		/// </summary>
		Square,
		/// <summary>
		/// rounded caps.
		/// </summary>
		Round
	}

	/// <summary>
	/// Line joint style.
	/// </summary>
	public enum JoinStyle
	{
		/// <summary>
		/// Beveled vertices.
		/// </summary>
		Bevel,
		/// <summary>
		/// Regular angular vertices.
		/// </summary>
		Miter,
		/// <summary>
		/// Rounded vertices.
		/// </summary>
		Round
	}

	/// <summary>
	/// Font style.
	/// </summary>
	[Flags]
	public enum FontStyle
	{
		/// <summary>
		/// Bold.
		/// </summary>
		Bold = 1 << 0,
		/// <summary>
		/// Italic.
		/// </summary>
		Italic = 1 << 1
	}

	/// <summary>
	/// Drawing unit. Try to use Dip when possible. It ensures the best results with different resolutions, display sizes and platforms.
	/// </summary>
	public enum DrawingUnit
	{
		/// <summary>
		/// Pixels. This is not guaranteed to be exact physical pixels on all platforms.
		/// </summary>
		Pixel,
		/// <summary>
		/// Resolution and display size independent units.
		/// </summary>
		Dip
	}
}
