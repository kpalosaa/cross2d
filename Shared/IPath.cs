using System;

namespace Uni2D
{
	public interface IPath : IDisposable
	{
		void BeginFigure(float x, float y);
		void EndFigure(bool close);

		void AddLine(float x, float y);
		void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle);

		void AddRect(float x, float y, float width, float height);
		void AddCircle(float xCenter, float yCenter, float radius);
		void AddEllipse(float xCenter, float yCenter, float hRadius, float vRadius);
	}
}
