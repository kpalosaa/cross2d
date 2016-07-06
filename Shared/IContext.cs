using System;
using Xamarin.Forms;

namespace Uni2D
{
	public interface IContext
	{
		void Clear();

		void DrawRect(float x, float y, float width, float height);

		void DrawLine(float x1, float y1, float x2, float y2);

		void DrawCircle(float xCenter, float yCenter, float radius);

		void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius);

		void SetFont(string name, int size, FontStyle style);

		Size MeasureText(string text);

		void DrawText(string text, float x, float y);
		void DrawText(string text, float x, float y, float width, float height, TextAlignment hAlignment, TextAlignment vAlignment);

		Color Color { get; set; }

		float StrokeWidth { get; set; }
		StrokeStyle StrokeStyle { get; set; }
		CapStyle StrokeCapStyle { get; set; }
		JoinStyle StrokeJoinStyle { get; set; }

		float Width { get; }
		float Height { get; }
	}
}
