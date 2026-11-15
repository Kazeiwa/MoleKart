using System.Collections.Generic;
using System.IO;

namespace Google.ProtocolBuffers;

public abstract class ExtendableMessageLite<TMessage, TBuilder> : GeneratedMessageLite<TMessage, TBuilder> where TMessage : GeneratedMessageLite<TMessage, TBuilder> where TBuilder : GeneratedBuilderLite<TMessage, TBuilder>
{
	protected class ExtensionWriter
	{
		private readonly IEnumerator<KeyValuePair<IFieldDescriptorLite, object>> iterator;

		private readonly FieldSet extensions;

		private KeyValuePair<IFieldDescriptorLite, object>? next;

		internal ExtensionWriter(ExtendableMessageLite<TMessage, TBuilder> message)
		{
		}

		public void WriteUntil(int end, CodedOutputStream output)
		{
		}
	}

	private readonly FieldSet extensions;

	internal FieldSet Extensions
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	protected bool ExtensionsAreInitialized
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override bool IsInitialized
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	protected int ExtensionsSerializedSize
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override bool Equals(object obj)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override int GetHashCode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public bool HasExtension<TExtension>(GeneratedExtensionLite<TMessage, TExtension> extension)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int GetExtensionCount<TExtension>(GeneratedExtensionLite<TMessage, IList<TExtension>> extension)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TExtension GetExtension<TExtension>(GeneratedExtensionLite<TMessage, TExtension> extension)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TExtension GetExtension<TExtension>(GeneratedExtensionLite<TMessage, IList<TExtension>> extension, int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected ExtensionWriter CreateExtensionWriter(ExtendableMessageLite<TMessage, TBuilder> message)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal void VerifyExtensionContainingType<TExtension>(GeneratedExtensionLite<TMessage, TExtension> extension)
	{
	}
}
