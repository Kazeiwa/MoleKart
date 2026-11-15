using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public class EnumLiteMap<TEnum> : IEnumLiteMap<IEnumLite>, IEnumLiteMap where TEnum : struct, IComparable, IFormattable
{
	private struct EnumValue : IEnumLite
	{
		private readonly TEnum value;

		int IEnumLite.Number
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		string IEnumLite.Name
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public EnumValue(TEnum value)
		{
		}
	}

	private readonly SortedList<int, IEnumLite> items;

	IEnumLite IEnumLiteMap.FindValueByNumber(int number)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IEnumLite FindValueByNumber(int number)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsValidValue(IEnumLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
