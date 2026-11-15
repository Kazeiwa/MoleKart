using System;
using System.Collections.Generic;
using System.Text;

namespace Google.ProtocolBuffers;

public sealed class UninitializedMessageException : Exception
{
	private readonly IList<string> missingFields;

	public IList<string> MissingFields => missingFields;

	private UninitializedMessageException(IList<string> missingFields)
		: base(BuildDescription(missingFields))
	{
		this.missingFields = new List<string>(missingFields);
	}

	public UninitializedMessageException(IMessageLite message)
		: base($"Message {message.GetType()} is missing required fields")
	{
		missingFields = new List<string>();
	}

	public InvalidProtocolBufferException AsInvalidProtocolBufferException()
	{
		return new InvalidProtocolBufferException(Message);
	}

	private static string BuildDescription(IEnumerable<string> missingFields)
	{
		StringBuilder stringBuilder = new StringBuilder("Message missing required fields: ");
		bool flag = true;
		foreach (string missingField in missingFields)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(missingField);
		}
		return stringBuilder.ToString();
	}
}
