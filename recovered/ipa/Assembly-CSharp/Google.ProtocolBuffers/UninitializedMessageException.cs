using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public sealed class UninitializedMessageException : Exception
{
	private readonly IList<string> missingFields;

	public IList<string> MissingFields
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private UninitializedMessageException(IList<string> missingFields)
	{
	}

	public UninitializedMessageException(IMessageLite message)
	{
	}

	public InvalidProtocolBufferException AsInvalidProtocolBufferException()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static string BuildDescription(IEnumerable<string> missingFields)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
