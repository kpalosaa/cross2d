using System;
using System.Numerics;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas;

namespace Cross2D
{
	public class Path : IPath
	{
		private CanvasPathBuilder pathBuilder;
		private CanvasGeometry geometry;

		public Path(ICanvasResourceCreator resourceCreator)
		{
			pathBuilder = new CanvasPathBuilder(resourceCreator);
			geometry = null;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (pathBuilder != null)
					pathBuilder.Dispose();
				if (geometry != null)
					geometry.Dispose();
			}
		}

		public void BeginFigure(float x, float y)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.BeginFigure(x, y);
		}

		public void EndFigure(bool close)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.EndFigure(close ? CanvasFigureLoop.Closed : CanvasFigureLoop.Open);
		}

		public void AddLine(float x, float y)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.AddLine(x, y);
		}

		public void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.AddArc(new Vector2(xCenter, yCenter), radius, radius, startAngle, endAngle);
		}

		public void AddRect(float x, float y, float width, float height)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.BeginFigure(x, y);
			pathBuilder.AddLine(x + width - 1, y);
			pathBuilder.AddLine(x + width - 1, y + height - 1);
			pathBuilder.AddLine(x, y + height - 1);
			pathBuilder.EndFigure(CanvasFigureLoop.Closed);
		}

		public void AddCircle(float xCenter, float yCenter, float radius)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.BeginFigure(xCenter + radius, yCenter);
			pathBuilder.AddArc(new Vector2(xCenter, yCenter), radius, radius, 0, (float)Math.PI * 2);
			pathBuilder.EndFigure(CanvasFigureLoop.Closed);
		}

		public void AddEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
			if (pathBuilder == null)
				throw new Exception("Path is closed");

			pathBuilder.BeginFigure(xCenter + hRadius, yCenter);
			pathBuilder.AddArc(new Vector2(xCenter, yCenter), hRadius, vRadius, 0, (float)Math.PI * 2);
			pathBuilder.EndFigure(CanvasFigureLoop.Closed);
		}

		internal CanvasGeometry GetGeometry()
		{
			if (geometry == null)
			{
				geometry = CanvasGeometry.CreatePath(pathBuilder);
				pathBuilder = null;
			}

			return geometry;
		}
	}
}
