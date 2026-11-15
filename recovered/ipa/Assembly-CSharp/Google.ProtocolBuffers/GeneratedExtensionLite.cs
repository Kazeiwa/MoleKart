using System.Collections.Generic;
using Google.ProtocolBuffers.Descriptors;

namespace Google.ProtocolBuffers;

public class GeneratedExtensionLite<TContainingType, TExtensionType> : IGeneratedExtensionLite where TContainingType : IMessageLite
{
	private readonly TContainingType containingTypeDefaultInstance;

	private readonly TExtensionType defaultValue;

	private readonly IMessageLite messageDefaultInstance;

	private readonly ExtensionDescriptorLite descriptor;

	private static readonly IList<object> Empty;

	object IGeneratedExtensionLite.ContainingType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IFieldDescriptorLite Descriptor
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public TExtensionType DefaultValue
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public TContainingType ContainingTypeDefaultInstance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int Number
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IMessageLite MessageDefaultInstance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	protected GeneratedExtensionLite(TContainingType containingTypeDefaultInstance, TExtensionType defaultValue, IMessageLite messageDefaultInstance, ExtensionDescriptorLite descriptor)
	{
	}

	public GeneratedExtensionLite(string fullName, TContainingType containingTypeDefaultInstance, TExtensionType defaultValue, IMessageLite messageDefaultInstance, IEnumLiteMap enumTypeMap, int number, FieldType type)
	{
	}

	protected GeneratedExtensionLite(string fullName, TContainingType containingTypeDefaultInstance, TExtensionType defaultValue, IMessageLite messageDefaultInstance, IEnumLiteMap enumTypeMap, int number, FieldType type, bool isPacked)
	{
	}

	public virtual object ToReflectionType(object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public object SingularToReflectionType(object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual object FromReflectionType(object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public object SingularFromReflectionType(object value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
