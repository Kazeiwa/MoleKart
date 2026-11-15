using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers;

public sealed class ExtensionRegistry
{
	private struct ExtensionIntPair : IEquatable<ExtensionIntPair>
	{
		private readonly object msgType;

		private readonly int number;

		internal ExtensionIntPair(object msgType, int number)
		{
			this.msgType = msgType;
			this.number = number;
		}

		public override int GetHashCode()
		{
			return msgType.GetHashCode() * 65535 + number;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ExtensionIntPair))
			{
				return false;
			}
			return Equals((ExtensionIntPair)obj);
		}

		public bool Equals(ExtensionIntPair other)
		{
			return msgType.Equals(other.msgType) && number == other.number;
		}
	}

	private readonly IDictionary<ExtensionIntPair, IGeneratedExtensionLite> extensionsByNumber;

	private readonly bool readOnly;

	private static readonly ExtensionRegistry empty = new ExtensionRegistry(new Dictionary<ExtensionIntPair, IGeneratedExtensionLite>(), readOnly: true);

	public static ExtensionRegistry Empty => empty;

	public IGeneratedExtensionLite this[IMessageLite containingType, int fieldNumber]
	{
		get
		{
			extensionsByNumber.TryGetValue(new ExtensionIntPair(containingType, fieldNumber), out var value);
			return value;
		}
	}

	private ExtensionRegistry(IDictionary<ExtensionIntPair, IGeneratedExtensionLite> extensionsByNumber, bool readOnly)
	{
		this.extensionsByNumber = extensionsByNumber;
		this.readOnly = readOnly;
	}

	public static ExtensionRegistry CreateInstance()
	{
		return new ExtensionRegistry(new Dictionary<ExtensionIntPair, IGeneratedExtensionLite>(), readOnly: false);
	}

	public ExtensionRegistry AsReadOnly()
	{
		return new ExtensionRegistry(extensionsByNumber, readOnly: true);
	}

	public void Add(IGeneratedExtensionLite extension)
	{
		if (readOnly)
		{
			throw new InvalidOperationException("Cannot add entries to a read-only extension registry");
		}
		extensionsByNumber.Add(new ExtensionIntPair(extension.ContainingType, extension.Number), extension);
	}
}
