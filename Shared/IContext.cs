using System;
using Xamarin.Forms;

namespace Uni2D
{
	public interface IContext
	{
		void DrawLine(float x1, float y1, float x2, float y2);

		void DrawCircle(float x, float y, float radius);

		Color Color { get; set; }
		float StrokeWidth { get; set; }
		StrokeStyle StrokeStyle { get; set; }
		CapStyle StrokeCapStyle { get; set; }

		float Width { get; }
		float Height { get; }
	}
}
