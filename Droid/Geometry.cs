using System;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Uni2D
{
	public sealed class Geometry : IGeometry
	{
		private Android.Graphics.Path path;

		public Geometry()
		{
			path = new Android.Graphics.Path();
		}

		private Geometry(Android.Graphics.Path path)
		{
			this.path = path;
		}

		internal static Geometry CreateCircle(float xCenter, float yCenter, float radius)
		{
			Android.Graphics.Path path = new Android.Graphics.Path();
			path.AddCircle(xCenter, yCenter, radius, Android.Graphics.Path.Direction.Cw);
			return new Geometry(path);
		}
	}
}
