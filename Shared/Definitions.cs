using System;

namespace Uni2D
{
	public enum CapStyle
	{
		Flat,
		Square,
		Round
	}

	public enum JoinStyle
	{
		Bevel,
		Miter,
		Round
	}

	[Flags]
	public enum FontStyle
	{
		Bold,
		Italic
	}
}
