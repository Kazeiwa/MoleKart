using System;
using System.IO;
using Google.ProtocolBuffers.Descriptors;

namespace Google.ProtocolBuffers;

public sealed class CodedInputStream
{
	internal const int DefaultRecursionLimit = 64;

	internal const int DefaultSizeLimit = 67108864;

	internal const int BufferSize = 4096;

	private readonly byte[] buffer;

	private int bufferSize;

	private int bufferSizeAfterLimit;

	private int bufferPos;

	private readonly Stream input;

	private uint lastTag;

	private int totalBytesRetired;

	private int currentLimit;

	private int recursionDepth;

	private int recursionLimit;

	private int sizeLimit;

	public bool ReachedLimit
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsAtEnd
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private CodedInputStream(byte[] buffer, int offset, int length)
	{
	}

	private CodedInputStream(Stream input)
	{
	}

	public static CodedInputStream CreateInstance(Stream input)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static CodedInputStream CreateInstance(byte[] buf)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static CodedInputStream CreateInstance(byte[] buf, int offset, int length)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public void CheckLastTagWas(uint value)
	{
	}

	[CLSCompliant(false)]
	public uint ReadTag()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public double ReadDouble()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float ReadFloat()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public ulong ReadUInt64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public long ReadInt64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int ReadInt32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public ulong ReadFixed64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public uint ReadFixed32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool ReadBool()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string ReadString()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ReadGroup(int fieldNumber, IBuilderLite builder, ExtensionRegistry extensionRegistry)
	{
	}

	[Obsolete]
	public void ReadUnknownGroup(int fieldNumber, IBuilderLite builder)
	{
	}

	public void ReadMessage(IBuilderLite builder, ExtensionRegistry extensionRegistry)
	{
	}

	public ByteString ReadBytes()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public uint ReadUInt32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int ReadEnum()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int ReadSFixed32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public long ReadSFixed64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int ReadSInt32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public long ReadSInt64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public object ReadPrimitiveField(FieldType fieldType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private uint SlowReadRawVarint32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public uint ReadRawVarint32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal static uint ReadRawVarint32(Stream input)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public ulong ReadRawVarint64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public uint ReadRawLittleEndian32()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public ulong ReadRawLittleEndian64()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static int DecodeZigZag32(uint n)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public static long DecodeZigZag64(ulong n)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int SetRecursionLimit(int limit)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int SetSizeLimit(int limit)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ResetSizeCounter()
	{
	}

	public int PushLimit(int byteLimit)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void RecomputeBufferSizeAfterLimit()
	{
	}

	public void PopLimit(int oldLimit)
	{
	}

	private bool RefillBuffer(bool mustSucceed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public byte ReadRawByte()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public byte[] ReadRawBytes(int size)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	public bool SkipField(uint tag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SkipMessage()
	{
	}

	public void SkipRawBytes(int size)
	{
	}

	private void SkipImpl(int amountToSkip)
	{
	}
}
