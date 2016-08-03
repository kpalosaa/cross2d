using System;
using Xamarin.Forms;

namespace Cross2D
{
	public interface IImage : IDisposable
	{
		ImageSource Source { get; set; }

		Size Size { get; }
		float Width { get; }
		float Height { get; }
	}
}
