using System;
using Google.ProtocolBuffers.Descriptors;

namespace Google.ProtocolBuffers;

public class ExtensionDescriptorLite : IComparable<IFieldDescriptorLite>, IFieldDescriptorLite
{
	private readonly string fullName;

	private readonly IEnumLiteMap enumTypeMap;

	private readonly int number;

	private readonly FieldType type;

	private readonly bool isRepeated;

	private readonly bool isPacked;

	private readonly MappedType mapType;

	private readonly object defaultValue;

	public string FullName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsRepeated
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsRequired
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsPacked
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsExtension
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool MessageSetWireFormat
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int FieldNumber
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumLiteMap EnumType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public FieldType FieldType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public MappedType MappedType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public object DefaultValue
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public ExtensionDescriptorLite(string fullName, IEnumLiteMap enumTypeMap, int number, FieldType type, object defaultValue, bool isRepeated, bool isPacked)
	{
	}

	public int CompareTo(IFieldDescriptorLite other)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
