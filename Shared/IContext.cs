using System;
using Xamarin.Forms;

namespace Uni2D
{
	public interface IContext
	{
		void Clear();

		void DrawRect(float x, float y, float width, float height);
		void FillRect(float x, float y, float width, float height);

		void DrawLine(float x1, float y1, float x2, float y2);

		void DrawCircle(float xCenter, float yCenter, float radius);
		void FillCircle(float xCenter, float yCenter, float radius);

		void DrawEllipse(float xCenter, float yCenter, float hRadius, float vRadius);
		void FillEllipse(float xCenter, float yCenter, float hRadius, float vRadius);

		void SetFont(string name, int size, FontStyle style = 0);
		void SetFont(int size, FontStyle style = 0);
		void SetFont(NamedSize namedSize, FontStyle style = 0);

		Size MeasureText(string text);

		void DrawText(string text, float x, float y);
		void DrawText(string text, float x, float y, float width, float height, TextAlignment hAlignment, TextAlignment vAlignment);

		Color Color { get; set; }

		float StrokeWidth { get; set; }
		CapStyle StrokeCapStyle { get; set; }
		JoinStyle StrokeJoinStyle { get; set; }

		float Width { get; }
		float Height { get; }
	}
}
