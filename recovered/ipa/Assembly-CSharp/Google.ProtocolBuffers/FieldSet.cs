using System.Collections;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

internal sealed class FieldSet
{
	private static readonly FieldSet defaultInstance;

	private IDictionary<IFieldDescriptorLite, object> fields;

	internal static FieldSet DefaultInstance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	internal IDictionary<IFieldDescriptorLite, object> AllFields
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	internal object this[IFieldDescriptorLite field]
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	internal object this[IFieldDescriptorLite field, int index]
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	internal bool IsInitialized
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int SerializedSize
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private FieldSet(IDictionary<IFieldDescriptorLite, object> fields)
	{
	}

	public static FieldSet CreateInstance()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal FieldSet MakeImmutable()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasField(IFieldDescriptorLite field)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal void Clear()
	{
	}

	internal void AddRepeatedField(IFieldDescriptorLite field, object value)
	{
	}

	internal IEnumerator<KeyValuePair<IFieldDescriptorLite, object>> GetEnumerator()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	internal bool IsInitializedWithRespectTo(IEnumerable typeFields)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ClearField(IFieldDescriptorLite field)
	{
	}

	public int GetRepeatedFieldCount(IFieldDescriptorLite field)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void MergeFrom(FieldSet other)
	{
	}

	private void MergeField(IFieldDescriptorLite field, object mergeValue)
	{
	}

	public void WriteTo(CodedOutputStream output)
	{
	}

	public void WriteField(IFieldDescriptorLite field, object value, CodedOutputStream output)
	{
	}

	private static void VerifyType(IFieldDescriptorLite field, object value)
	{
	}
}
