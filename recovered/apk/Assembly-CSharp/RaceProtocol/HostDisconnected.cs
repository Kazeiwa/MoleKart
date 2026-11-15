using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class HostDisconnected : GeneratedMessageLite<HostDisconnected, HostDisconnected.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<HostDisconnected, Builder>
	{
		private HostDisconnected result = new HostDisconnected();

		protected override Builder ThisBuilder => this;

		protected override HostDisconnected MessageBeingBuilt => result;

		public override HostDisconnected DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new HostDisconnected();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override HostDisconnected BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			HostDisconnected hostDisconnected = result;
			result = null;
			return hostDisconnected;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is HostDisconnected)
			{
				return MergeFrom((HostDisconnected)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(HostDisconnected other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			return this;
		}

		public override Builder MergeFrom(CodedInputStream input)
		{
			return MergeFrom(input, ExtensionRegistry.Empty);
		}

		public override Builder MergeFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			while (true)
			{
				uint num = input.ReadTag();
				if (num == 0)
				{
					return this;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					break;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
			return this;
		}
	}

	private static readonly HostDisconnected defaultInstance;

	private int memoizedSerializedSize = -1;

	public static HostDisconnected DefaultInstance => defaultInstance;

	public override HostDisconnected DefaultInstanceForType => defaultInstance;

	protected override HostDisconnected ThisMessage => this;

	public override bool IsInitialized => true;

	public override int SerializedSize
	{
		get
		{
			int num = memoizedSerializedSize;
			if (num != -1)
			{
				return num;
			}
			return memoizedSerializedSize = 0;
		}
	}

	static HostDisconnected()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
	}

	public override int GetHashCode()
	{
		return GetType().GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (!(obj is HostDisconnected))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static HostDisconnected ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static HostDisconnected ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static HostDisconnected ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static HostDisconnected ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static HostDisconnected ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static HostDisconnected ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static HostDisconnected ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static HostDisconnected ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static HostDisconnected ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static HostDisconnected ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static Builder CreateBuilder()
	{
		return new Builder();
	}

	public override Builder ToBuilder()
	{
		return CreateBuilder(this);
	}

	public override Builder CreateBuilderForType()
	{
		return new Builder();
	}

	public static Builder CreateBuilder(HostDisconnected prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
