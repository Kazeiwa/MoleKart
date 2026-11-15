using System.IO;

namespace Google.ProtocolBuffers;

public abstract class AbstractMessageLite<TMessage, TBuilder> : IMessageLite<TMessage, TBuilder> where TMessage : AbstractMessageLite<TMessage, TBuilder> where TBuilder : AbstractBuilderLite<TMessage, TBuilder>
{
	public override IMessageLite WeakDefaultInstanceForType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override ByteString ToByteString()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override byte[] ToByteArray()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void WriteTo(Stream output)
	{
	}

	public override void WriteDelimitedTo(Stream output)
	{
	}

	public override IBuilderLite WeakCreateBuilderForType()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakToBuilder()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
