using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.ProtocolBuffers.Collections;

public static class Lists
{
	public static IList<T> AsReadOnly<T>(IList<T> list)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static bool Equals<T>(IList<T> left, IList<T> right)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int GetHashCode<T>(IList<T> list)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
public static class Lists<T>
{
	private static readonly ReadOnlyCollection<T> empty;

	public static ReadOnlyCollection<T> Empty
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static IList<T> AsReadOnly(IList<T> list)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
