using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public static class ThrowHelper
{
	public static void ThrowIfNull(object value, string name)
	{
	}

	public static void ThrowIfNull(object value)
	{
	}

	public static void ThrowIfAnyNull<T>(IEnumerable<T> sequence)
	{
	}
}
