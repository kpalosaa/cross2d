using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni2D
{
	public class Path : IPath
	{
		public Path()
		{
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
			}
		}

		public void BeginFigure(float x, float y)
		{
		}

		public void EndFigure(bool close)
		{
		}

		public void AddLine(float x, float y)
		{
		}

		public void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle)
		{
		}

		public void AddRect(float x, float y, float width, float height)
		{
		}

		public void AddCircle(float xCenter, float yCenter, float radius)
		{
		}

		public void AddEllipse(float xCenter, float yCenter, float hRadius, float vRadius)
		{
		}
	}
}
