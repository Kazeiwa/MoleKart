using System.Collections.Generic;
using System.IO;

namespace Google.ProtocolBuffers;

public abstract class GeneratedMessageLite<TMessage, TBuilder> : AbstractMessageLite<TMessage, TBuilder> where TMessage : GeneratedMessageLite<TMessage, TBuilder> where TBuilder : GeneratedBuilderLite<TMessage, TBuilder>
{
	protected abstract TMessage ThisMessage { get; }

	public sealed override string ToString()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected static void PrintField<T>(string name, IList<T> value, TextWriter writer)
	{
	}

	protected static void PrintField(string name, bool hasValue, object value, TextWriter writer)
	{
	}

	private static void EscapeBytes(IEnumerable<byte> input, TextWriter writer)
	{
	}
}
