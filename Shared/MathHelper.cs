using System;

namespace Cross2D
{
	public static class MathHelper
	{
		public const float DegToRad = (float)Math.PI / 180.0f;
		public const float RadToDeg = 180.0f / (float)Math.PI;

		public static float ConvertDegToRad(float degrees)
		{
			return degrees * DegToRad;
		}

		public static float ConvertRadToDeg(float radians)
		{
			return radians * RadToDeg;
		}
	}
}
