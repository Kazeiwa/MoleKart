using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public abstract class GeneratedBuilderLite<TMessage, TBuilder> : AbstractBuilderLite<TMessage, TBuilder> where TMessage : GeneratedMessageLite<TMessage, TBuilder> where TBuilder : GeneratedBuilderLite<TMessage, TBuilder>
{
	protected abstract TMessage MessageBeingBuilt { get; }

	public override bool IsInitialized
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override TBuilder MergeFrom(IMessageLite other)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public abstract TBuilder MergeFrom(TMessage other);

	protected void AddRange<T>(IEnumerable<T> source, IList<T> destination)
	{
	}

	[CLSCompliant(false)]
	protected virtual bool ParseUnknownField(CodedInputStream input, ExtensionRegistry extensionRegistry, uint tag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TMessage BuildParsed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TMessage Build()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
