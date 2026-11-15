using System;
using System.IO;
using Google.ProtocolBuffers.Descriptors;

namespace Google.ProtocolBuffers;

public sealed class CodedOutputStream
{
	public sealed class OutOfSpaceException : IOException
	{
		internal OutOfSpaceException()
		{
		}
	}

	private const int LittleEndian64Size = 8;

	private const int LittleEndian32Size = 4;

	public static readonly int DefaultBufferSize;

	private readonly byte[] buffer;

	private readonly int limit;

	private int position;

	private readonly Stream output;

	public int SpaceLeft
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private CodedOutputStream(byte[] buffer, int offset, int length)
	{
	}

	private CodedOutputStream(Stream output, byte[] buffer)
	{
	}

	public static CodedOutputStream CreateInstance(Stream output)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static CodedOutputStream CreateInstance(Stream output, int bufferSize)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static CodedOutputStream CreateInstance(byte[] flatArray)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static CodedOutputStream CreateInstance(byte[] flatArray, int offset, int length)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void WriteDouble(int fieldNumber, double value)
	{
	}

	public void WriteFloat(int fieldNumber, float value)
	{
	}

	[CLSCompliant(false)]
	public void WriteUInt64(int fieldNumber, ulong value)
	{
	}

	public void WriteInt64(int fieldNumber, long value)
	{
	}

	public void WriteInt32(int fieldNumber, int value)
	{
	}

	[CLSCompliant(false)]
	public void WriteFixed64(int fieldNumber, ulong value)
	{
	}

	[CLSCompliant(false)]
	public void WriteFixed32(int fieldNumber, uint value)
	{
	}

	public void WriteBool(int fieldNumber, bool value)
	{
	}

	public void WriteString(int fieldNumber, string value)
	{
	}

	public void WriteGroup(int fieldNumber, IMessageLite value)
	{
	}

	[Obsolete]
	public void WriteUnknownGroup(int fieldNumber, IMessageLite value)
	{
	}

	public void WriteMessage(int fieldNumber, IMessageLite value)
	{
	}

	public void WriteBytes(int fieldNumber, ByteString value)
	{
	}

	[CLSCompliant(false)]
	public void WriteUInt32(int fieldNumber, uint value)
	{
	}

	public void WriteEnum(int fieldNumber, int value)
	{
	}

	public void WriteSFixed32(int fieldNumber, int value)
	{
	}

	public void WriteSFixed64(int fieldNumber, long value)
	{
	}

	public void WriteSInt32(int fieldNumber, int value)
	{
	}

	public void WriteSInt64(int fieldNumber, long value)
	{
	}

	public void WriteMessageSetExtension(int fieldNumber, IMessageLite value)
	{
	}

	public void WriteRawMessageSetExtension(int fieldNumber, ByteString value)
	{
	}

	public void WriteField(FieldType fieldType, int fieldNumber, object value)
	{
	}

	public void WriteFieldNoTag(FieldType fieldType, object value)
	{
	}

	public void WriteDoubleNoTag(double value)
	{
	}

	public void WriteFloatNoTag(float value)
	{
	}

	[CLSCompliant(false)]
	public void WriteUInt64NoTag(ulong value)
	{
	}

	public void WriteInt64NoTag(long value)
	{
	}

	public void WriteInt32NoTag(int value)
	{
	}

	[CLSCompliant(false)]
	public void WriteFixed64NoTag(ulong value)
	{
	}

	[CLSCompliant(false)]
	public void WriteFixed32NoTag(uint value)
	{
	}

	public void WriteBoolNoTag(bool value)
	{
	}

	public void WriteStringNoTag(string value)
	{
	}

	public void WriteGroupNoTag(IMessageLite value)
	{
	}

	public void WriteMessageNoTag(IMessageLite value)
	{
	}

	public void WriteBytesNoTag(ByteString value)
	{
	}

	[CLSCompliant(false)]
	public void WriteUInt32NoTag(uint value)
	{
	}

	public void WriteEnumNoTag(int value)
	{
	}

	public void WriteSFixed32NoTag(int value)
	{
	}

	public void WriteSFixed64NoTag(long value)
	{
	}

	public void WriteSInt32NoTag(int value)
	{
	}

	public void WriteSInt64NoTag(long value)
	{
	}

	[CLSCompliant(false)]
	public void WriteTag(int fieldNumber, WireFormat.WireType type)
	{
	}

	private void SlowWriteRawVarint32(uint value)
	{
	}

	[CLSCompliant(false)]
	public void WriteRawVarint32(uint value)
	{
	}

	[CLSCompliant(false)]
	public void WriteRawVarint64(ulong value)
	{
	}

	[CLSCompliant(false)]
	public void WriteRawLittleEndian32(uint value)
	{
	}

	[CLSCompliant(false)]
	public void WriteRawLittleEndian64(ulong value)
	{
	}

	public void WriteRawByte(byte value)
	{
	}

	[CLSCompliant(false)]
	public void WriteRawByte(uint value)
	{
	}

	public void WriteRawBytes(byte[] value)
	{
	}

	public void WriteRawBytes(byte[] value, int offset, int length)
	{
	}

	public static int ComputeDoubleSize(int fieldNumber, double value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeFloatSize(int fieldNumber, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeUInt64Size(int fieldNumber, ulong value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeInt64Size(int fieldNumber, long value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeInt32Size(int fieldNumber, int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeFixed64Size(int fieldNumber, ulong value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeFixed32Size(int fieldNumber, uint value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeBoolSize(int fieldNumber, bool value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeStringSize(int fieldNumber, string value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeGroupSize(int fieldNumber, IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[Obsolete]
	public static int ComputeUnknownGroupSize(int fieldNumber, IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeMessageSize(int fieldNumber, IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeBytesSize(int fieldNumber, ByteString value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeUInt32Size(int fieldNumber, uint value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeEnumSize(int fieldNumber, int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSFixed32Size(int fieldNumber, int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSFixed64Size(int fieldNumber, long value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSInt32Size(int fieldNumber, int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSInt64Size(int fieldNumber, long value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeDoubleSizeNoTag(double value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeFloatSizeNoTag(float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeUInt64SizeNoTag(ulong value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeInt64SizeNoTag(long value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeInt32SizeNoTag(int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeFixed64SizeNoTag(ulong value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeFixed32SizeNoTag(uint value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeBoolSizeNoTag(bool value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeStringSizeNoTag(string value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeGroupSizeNoTag(IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[Obsolete]
	public static int ComputeUnknownGroupSizeNoTag(IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeMessageSizeNoTag(IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeBytesSizeNoTag(ByteString value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeUInt32SizeNoTag(uint value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeEnumSizeNoTag(int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSFixed32SizeNoTag(int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSFixed64SizeNoTag(long value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSInt32SizeNoTag(int value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeSInt64SizeNoTag(long value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeMessageSetExtensionSize(int fieldNumber, IMessageLite value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeRawMessageSetExtensionSize(int fieldNumber, ByteString value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeRawVarint32Size(uint value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int ComputeRawVarint64Size(ulong value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeFieldSize(FieldType fieldType, int fieldNumber, object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeFieldSizeNoTag(FieldType fieldType, object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int ComputeTagSize(int fieldNumber)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static uint EncodeZigZag32(int n)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static ulong EncodeZigZag64(long n)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void RefreshBuffer()
	{
	}

	public void Flush()
	{
	}

	public void CheckNoSpaceLeft()
	{
	}
}
