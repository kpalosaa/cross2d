﻿using System;

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
		Bold = 1 << 0,
		Italic = 1 << 1
	}
}
