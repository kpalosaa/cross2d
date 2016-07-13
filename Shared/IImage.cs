using System;
using Xamarin.Forms;

namespace Uni2D
{
	public interface IImage : IDisposable
	{
		ImageSource Source { get; set; }

		Size Size { get; }
		float Width { get; }
		float Height { get; }
	}
}
