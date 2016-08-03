using System;

namespace Cross2D
{
	/// <summary>
	/// Path. Path can be modified until the first drawing. After that it's locked and modification is not allowed.
	/// </summary>
	public interface IPath : IDisposable
	{
		/// <summary>
		/// Begin a new figure.
		/// </summary>
		/// <param name="x">X coordinate where to start the figure.</param>
		/// <param name="y">Y coordinate where to start the figure.</param>
		void BeginFigure(float x, float y);

		/// <summary>
		/// End the figure.
		/// </summary>
		/// <param name="close">Set true to join the last point to the starting point as a line.</param>
		void EndFigure(bool close);

		/// <summary>
		/// Add line from the current point to the specified point.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		void AddLine(float x, float y);

		/// <summary>
		/// Add arc. This should start from the current point.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the arc.</param>
		/// <param name="yCenter">Y coordinate of the center of the arc.</param>
		/// <param name="radius">Arc radius.</param>
		/// <param name="startAngle">Start angle in radians.</param>
		/// <param name="endAngle">End angle in radians.</param>
		void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle);

		/// <summary>
		/// Add rectangle and close the figure. Previous figure should be closed before calling this function.
		/// </summary>
		/// <param name="x">X coordinate of the upper left corner.</param>
		/// <param name="y">Y coordinate of the upper left corner.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		void AddRect(float x, float y, float width, float height);

		/// <summary>
		/// Add circle and close the figure. Previous figure should be closed before calling this function.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the circle.</param>
		/// <param name="yCenter">Y coordinate of the center of the circle.</param>
		/// <param name="radius">Radius.</param>
		void AddCircle(float xCenter, float yCenter, float radius);

		/// <summary>
		/// Add ellipse and close the figure. Previous figure should be closed before calling this function.
		/// </summary>
		/// <param name="xCenter">X coordinate of the center of the circle.</param>
		/// <param name="yCenter">Y coordinate of the center of the circle.</param>
		/// <param name="hRadius">Horizontal radius.</param>
		/// <param name="vRadius">Vertical radius.</param>
		void AddEllipse(float xCenter, float yCenter, float hRadius, float vRadius);
	}
}
