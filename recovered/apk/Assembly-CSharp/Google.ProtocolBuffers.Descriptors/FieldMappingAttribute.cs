using System;
using System.Collections.Generic;
using System.Reflection;
using Google.ProtocolBuffers.Collections;

namespace Google.ProtocolBuffers.Descriptors;

[AttributeUsage(AttributeTargets.Field)]
internal sealed class FieldMappingAttribute : Attribute
{
	private static readonly IDictionary<FieldType, FieldMappingAttribute> FieldTypeToMappedTypeMap = MapFieldTypes();

	internal MappedType MappedType { get; private set; }

	internal WireFormat.WireType WireType { get; private set; }

	internal FieldMappingAttribute(MappedType mappedType, WireFormat.WireType wireType)
	{
		MappedType = mappedType;
		WireType = wireType;
	}

	private static IDictionary<FieldType, FieldMappingAttribute> MapFieldTypes()
	{
		Dictionary<FieldType, FieldMappingAttribute> dictionary = new Dictionary<FieldType, FieldMappingAttribute>();
		FieldInfo[] fields = typeof(FieldType).GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			FieldType key = (FieldType)(int)fieldInfo.GetValue(null);
			FieldMappingAttribute value = (FieldMappingAttribute)fieldInfo.GetCustomAttributes(typeof(FieldMappingAttribute), inherit: false)[0];
			dictionary[key] = value;
		}
		return Dictionaries.AsReadOnly(dictionary);
	}

	internal static MappedType MappedTypeFromFieldType(FieldType type)
	{
		return FieldTypeToMappedTypeMap[type].MappedType;
	}

	internal static WireFormat.WireType WireTypeFromFieldType(FieldType type, bool packed)
	{
		return (!packed) ? FieldTypeToMappedTypeMap[type].WireType : WireFormat.WireType.LengthDelimited;
	}
}
