using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public abstract class ExtendableBuilderLite<TMessage, TBuilder> : GeneratedBuilderLite<TMessage, TBuilder> where TMessage : ExtendableMessageLite<TMessage, TBuilder> where TBuilder : GeneratedBuilderLite<TMessage, TBuilder>
{
	public object this[IFieldDescriptorLite field, int index]
	{
		set
		{
		}
	}

	public object this[IFieldDescriptorLite field]
	{
		set
		{
		}
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

	public TBuilder SetExtension<TExtension>(GeneratedExtensionLite<TMessage, TExtension> extension, TExtension value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TBuilder SetExtension<TExtension>(GeneratedExtensionLite<TMessage, IList<TExtension>> extension, int index, TExtension value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TBuilder AddExtension<TExtension>(GeneratedExtensionLite<TMessage, IList<TExtension>> extension, TExtension value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TBuilder ClearExtension<TExtension>(GeneratedExtensionLite<TMessage, TExtension> extension)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[CLSCompliant(false)]
	protected override bool ParseUnknownField(CodedInputStream input, ExtensionRegistry extensionRegistry, uint tag)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TBuilder ClearField(IFieldDescriptorLite field)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public TBuilder AddRepeatedField(IFieldDescriptorLite field, object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void MergeExtensionFields(ExtendableMessageLite<TMessage, TBuilder> other)
	{
	}
}
