using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public class EnumLiteMap<TEnum> : IEnumLiteMap<IEnumLite>, IEnumLiteMap where TEnum : struct, IComparable, IFormattable
{
	private struct EnumValue : IEnumLite
	{
		private readonly TEnum value;

		int IEnumLite.Number => Convert.ToInt32(value);

		string IEnumLite.Name => value.ToString();

		public EnumValue(TEnum value)
		{
			this.value = value;
		}
	}

	private readonly SortedList<int, IEnumLite> items;

	public EnumLiteMap()
	{
		items = new SortedList<int, IEnumLite>();
		foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
		{
			items.Add(Convert.ToInt32(value), new EnumValue(value));
		}
	}

	IEnumLite IEnumLiteMap.FindValueByNumber(int number)
	{
		return FindValueByNumber(number);
	}

	public IEnumLite FindValueByNumber(int number)
	{
		object result;
		if (items.TryGetValue(number, out var value))
		{
			IEnumLite enumLite = value;
			result = enumLite;
		}
		else
		{
			result = null;
		}
		return (IEnumLite)result;
	}

	public bool IsValidValue(IEnumLite value)
	{
		return items.ContainsKey(value.Number);
	}
}
