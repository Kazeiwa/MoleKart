using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Google.ProtocolBuffers.Descriptors;

[AttributeUsage(AttributeTargets.Field)]
internal sealed class FieldMappingAttribute : Attribute
{
	private static readonly IDictionary<FieldType, FieldMappingAttribute> FieldTypeToMappedTypeMap;

	internal MappedType MappedType
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		private set
		{
		}
	}

	internal WireFormat.WireType WireType
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		private set
		{
		}
	}

	internal FieldMappingAttribute(MappedType mappedType, WireFormat.WireType wireType)
	{
	}

	private static IDictionary<FieldType, FieldMappingAttribute> MapFieldTypes()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal static MappedType MappedTypeFromFieldType(FieldType type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal static WireFormat.WireType WireTypeFromFieldType(FieldType type, bool packed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
