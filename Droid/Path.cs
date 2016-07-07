using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Uni2D
{
	public sealed class Path : IPath
	{
		private Android.Graphics.Path path;

		public Path()
		{
			path = new Android.Graphics.Path();
		}

		public void BeginFigure(float x, float y)
		{
			path.MoveTo(x, y);
		}

		public void EndFigure(bool close)
		{
			if (close)
				path.Close();
		}

		public void AddLine(float x, float y)
		{
			path.LineTo(x, y);
		}

		public void AddArc(float xCenter, float yCenter, float radius, float startAngle, float endAngle)
		{
			path.AddArc(xCenter - radius, yCenter - radius, xCenter + radius, yCenter + radius, startAngle, endAngle - startAngle);
		}
	}
}
