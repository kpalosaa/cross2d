using System;
using CoreGraphics;

namespace Uni2D
{
	public class Path : IPath
	{
		private CGPath path;

		public Path()
		{
			path = new CGPath();
		}

		public void BeginFigure(float x, float y)
		{
			path.MoveToPoint(x, y);
		}

		public void EndFigure(bool close)
		{
			path.CloseSubpath();
		}

		public void AddLine(float x, float y)
		{
			path.AddLineToPoint(x, y);
		}

		public void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle)
		{
			path.AddArc(xCenter, yCenter, radius, startAngle, endAngle, true);
		}

		public void AddRect(float x, float y, float width, float height)
		{
			path.AddRect(new CGRect(x, y, width, height));
		}

		public void AddCircle(float xCenter, float yCenter, float radius)
		{
			path.AddEllipseInRect(new CGRect(xCenter - radius, yCenter - radius, 2 * radius, 2 * radius));
		}

		public void AddEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			path.AddEllipseInRect(new CGRect(xCenter - hRadius, yCenter - vRadius, 2 * hRadius, 2 * vRadius));
		}

		internal CGPath NativePath { get { return path; } }
	}
}
