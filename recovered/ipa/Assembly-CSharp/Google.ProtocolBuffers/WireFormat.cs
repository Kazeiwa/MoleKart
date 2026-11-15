using System;

namespace Google.ProtocolBuffers;

public static class WireFormat
{
	[CLSCompliant(false)]
	public enum WireType : uint
	{
		Varint,
		Fixed64,
		LengthDelimited,
		StartGroup,
		EndGroup,
		Fixed32
	}

	internal static class MessageSetField
	{
		internal const int Item = 1;

		internal const int TypeID = 2;

		internal const int Message = 3;
	}

	internal static class MessageSetTag
	{
		internal static readonly uint ItemStart;

		internal static readonly uint ItemEnd;

		internal static readonly uint TypeID;

		internal static readonly uint Message;
	}

	internal const int Fixed32Size = 4;

	internal const int Fixed64Size = 8;

	internal const int SFixed32Size = 4;

	internal const int SFixed64Size = 8;

	internal const int FloatSize = 4;

	internal const int DoubleSize = 8;

	internal const int BoolSize = 1;

	private const int TagTypeBits = 3;

	private const uint TagTypeMask = 7u;

	[CLSCompliant(false)]
	public static WireType GetTagWireType(uint tag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static bool IsEndGroupTag(uint tag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int GetTagFieldNumber(uint tag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static uint MakeTag(int fieldNumber, WireType wireType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
