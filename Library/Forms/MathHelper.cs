using System;
using Xamarin.Forms;

namespace Cross2D
{
	/// <summary>
	/// Helper functions.
	/// </summary>
	public static class MathHelper
	{
		/// <summary>
		/// Multiply by this to convert degrees to radians.
		/// </summary>
		public const float DegToRad = (float)Math.PI / 180.0f;

		/// <summary>
		/// Multiply by this to convert radians to degrees.
		/// </summary>
		public const float RadToDeg = 180.0f / (float)Math.PI;

		/// <summary>
		/// Convert degrees to radians
		/// </summary>
		/// <param name="degrees">Value in degrees.</param>
		/// <returns>Value in radians</returns>
		public static float ConvertDegToRad(float degrees)
		{
			return degrees * DegToRad;
		}

		/// <summary>
		/// Convert radians to degrees.
		/// </summary>
		/// <param name="radians">Value in radians.</param>
		/// <returns>Value in degrees</returns>
		public static float ConvertRadToDeg(float radians)
		{
			return radians * RadToDeg;
		}

		internal static Rectangle Fit(Size image, Rectangle boundingBox)
		{
			double widthScale = boundingBox.Width / image.Width;
			double heightScale = boundingBox.Height / image.Height;

			double scale = Math.Min(widthScale, heightScale);

			double width = image.Width * scale;
			double height = image.Height * scale;

			return new Rectangle(
				boundingBox.Left + (boundingBox.Width - width) / 2,
				boundingBox.Top + (boundingBox.Height - height) / 2,
				width,
				height);
		}

		internal static Rectangle Fill(Rectangle image, Size boundingBox)
		{
			double widthScale = image.Width / boundingBox.Width;
			double heightScale = image.Height / boundingBox.Height;

			double scale = Math.Min(widthScale, heightScale);

			double width = boundingBox.Width * scale;
			double height = boundingBox.Height * scale;

			return new Rectangle(
				image.Left + (image.Width - width) / 2,
				image.Top + (image.Height - height) / 2,
				width,
				height);
		}
	}
}
