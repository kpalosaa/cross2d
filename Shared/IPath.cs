using System;

namespace Uni2D
{
	public interface IPath
	{
		void BeginFigure(float x, float y);
		void EndFigure(bool close);

		void AddLine(float x, float y);
		void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle);
	}
}
