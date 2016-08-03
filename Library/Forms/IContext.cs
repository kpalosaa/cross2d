using System;
using Xamarin.Forms;

namespace Cross2D
{
	/// <summary>
	/// Drawing context. This is where all the drawing happens. Drawing unit is the resolution independent platform specific unit except on Android where it is pixels.
	/// </summary>
	public interface IContext : IDisposable
	{
		/// <summary>
		/// Clear entire control.
		/// </summary>
		void Clear();

		/// <summary>
		/// Draw rectangle.
		/// </summary>
		/// <param name="x">X coordinate of the upper left corner.</param>
		/// <param name="y">Y coordinate of the upper left corner.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		void DrawRect(float x, float y, float width, float height);

		/// <summary>
		/// Fill rectangle.
		/// </summary>
		/// <param name="x">X coordinate of the upper left corner.</param>
		/// <param name="y">Y coordinate of the upper left corner.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		void FillRect(float x, float y, float width, float height);

		/// <summary>
		/// Draw line.
		/// </summary>
		/// <param name="x1">X coordinate of the starting point.</param>
		/// <param name="y1">Y coordinate of the starting point.</param>
		/// <param name="x2">X coordinate of the ending point.</param>
		/// <param name="y2">Y coordinate of the ending point.</param>
		void DrawLine(float x1, float y1, float x2, float y2);

		/// <summary>
		/// Draw circle.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the circle.</param>
		/// <param name="yCenter">Y coordinate of the center of the circle.</param>
		/// <param name="radius">Radius.</param>
		void DrawCircle(float xCenter, float yCenter, float radius);

		/// <summary>
		/// Fill circle.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the circle.</param>
		/// <param name="yCenter">Y coordinate of the center of the circle.</param>
		/// <param name="radius">Radius.</param>
		void FillCircle(float xCenter, float yCenter, float radius);

		/// <summary>
		/// Draw ellipse.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the circle.</param>
		/// <param name="yCenter">Y coordinate of the center of the circle.</param>
		/// <param name="hRadius">Horizontal radius.</param>
		/// <param name="vRadius">Vertical radius.</param>
		void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius);

		/// <summary>
		/// Fill ellipse.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the circle.</param>
		/// <param name="yCenter">Y coordinate of the center of the circle.</param>
		/// <param name="hRadius">Horizontal radius.</param>
		/// <param name="vRadius">Vertical radius.</param>
		void FillEllipse(float xCenter, float yCenter, float hRadius, float vRadius);

		/// <summary>
		/// Set the current font.
		/// </summary>
		/// <param name="font">Font.</param>
		void SetFont(IFont font);

		/// <summary>
		/// Measure text size.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <returns>Size</returns>
		Size MeasureText(string text);

		/// <summary>
		/// Draw text.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="x">X coordinate of the upper left corner of the text.</param>
		/// <param name="y">Y coordinate of the upper left corner of the text.</param>
		void DrawText(string text, float x, float y);

		/// <summary>
		/// Draw text inside the specified box with specified alignments.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="x">X coordinate of the box.</param>
		/// <param name="y">Y coordinate of the box.</param>
		/// <param name="width">Width of the box.</param>
		/// <param name="height">Height of the box.</param>
		/// <param name="hAlignment">Horizontal text alignment.</param>
		/// <param name="vAlignment">Vertical text alignment.</param>
		void DrawText(string text, float x, float y, float width, float height, TextAlignment hAlignment, TextAlignment vAlignment);

		/// <summary>
		/// Draw the path.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="x">X coordinate of the starting point.</param>
		/// <param name="y">Y coordinate of the starting point.</param>
		void DrawPath(IPath path, float x, float y);

		/// <summary>
		/// Fill the path.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="x">X coordinate of the starting point.</param>
		/// <param name="y">Y coordinate of the starting point.</param>
		void FillPath(IPath path, float x, float y);

		/// <summary>
		/// Draw image.
		/// </summary>
		/// <param name="image">Image.</param>
		/// <param name="x">X coordinate of the upper left corner.</param>
		/// <param name="y">Y coordinate of the upper left corner.</param>
		/// <param name="unit">Drawing unit.</param>
		void DrawImage(IImage image, float x, float y, DrawingUnit unit = DrawingUnit.Dip);

		/// <summary>
		/// Scale and draw image.
		/// </summary>
		/// <param name="image">Image.</param>
		/// <param name="x">X coordinate of the upper left corner.</param>
		/// <param name="y">Y coordinate of the upper left corner.</param>
		/// <param name="width">Image width.</param>
		/// <param name="height">Image height.</param>
		/// <param name="aspect">Aspect of the image.</param>
		void DrawImage(IImage image, float x, float y, float width, float height, Aspect aspect);

		/// <summary>
		/// Save current transform matrix.
		/// </summary>
		void Save();

		/// <summary>
		/// Restore transform matrix.
		/// </summary>
		void Restore();

		/// <summary>
		/// Translate current transform matrix.
		/// </summary>
		/// <param name="sx">X amount of translate.</param>
		/// <param name="sy">Y amount of translate.</param>
		void Translate(float sx, float sy);

		/// <summary>
		/// Scale current transform matrix.
		/// </summary>
		/// <param name="dx">X amount of scale.</param>
		/// <param name="dy">Y amount of scale.</param>
		void Scale(float dx, float dy);

		/// <summary>
		/// Rotate current transform matrix.
		/// </summary>
		/// <param name="angle">Angle in radians.</param>
		void Rotate(float angle);

		/// <summary>
		/// Convert dips to pixels.
		/// </summary>
		/// <param name="dip">Dips.</param>
		/// <returns>Pixels</returns>
		float DipToPixel(float dip);

		/// <summary>
		/// Convert pixels to dips.
		/// </summary>
		/// <param name="pixel">Pixels.</param>
		/// <returns>Dips</returns>
		float PixelToDip(float pixel);

		/// <summary>
		/// Convert dips to pixels.
		/// </summary>
		/// <param name="dip">Dips.</param>
		/// <returns>Pixels</returns>
		Size DipToPixel(Size dip);

		/// <summary>
		/// Convert pixels to dips.
		/// </summary>
		/// <param name="pixel">Pixels.</param>
		/// <returns>Dips</returns>
		Size PixelToDip(Size pixel);

		/// <summary>
		/// Current drawing and filling color.
		/// </summary>
		Color Color { get; set; }

		/// <summary>
		/// Current stroke width.
		/// </summary>
		float StrokeWidth { get; set; }

		/// <summary>
		/// Current stroke cap style.
		/// </summary>
		CapStyle StrokeCapStyle { get; set; }

		/// <summary>
		/// Current stroke join style.
		/// </summary>
		JoinStyle StrokeJoinStyle { get; set; }

		/// <summary>
		/// Drawing context width.
		/// </summary>
		float Width { get; }

		/// <summary>
		/// Drawing context height.
		/// </summary>
		float Height { get; }
	}
}
