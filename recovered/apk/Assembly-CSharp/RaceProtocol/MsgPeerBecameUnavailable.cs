using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPeerBecameUnavailable : GeneratedMessageLite<MsgPeerBecameUnavailable, MsgPeerBecameUnavailable.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPeerBecameUnavailable, Builder>
	{
		private MsgPeerBecameUnavailable result = new MsgPeerBecameUnavailable();

		protected override Builder ThisBuilder => this;

		protected override MsgPeerBecameUnavailable MessageBeingBuilt => result;

		public override MsgPeerBecameUnavailable DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgPeerBecameUnavailable();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPeerBecameUnavailable BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPeerBecameUnavailable msgPeerBecameUnavailable = result;
			result = null;
			return msgPeerBecameUnavailable;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPeerBecameUnavailable)
			{
				return MergeFrom((MsgPeerBecameUnavailable)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPeerBecameUnavailable other)
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

	private static readonly MsgPeerBecameUnavailable defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgPeerBecameUnavailable DefaultInstance => defaultInstance;

	public override MsgPeerBecameUnavailable DefaultInstanceForType => defaultInstance;

	protected override MsgPeerBecameUnavailable ThisMessage => this;

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

	static MsgPeerBecameUnavailable()
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
		if (!(obj is MsgPeerBecameUnavailable))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgPeerBecameUnavailable ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPeerBecameUnavailable ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPeerBecameUnavailable prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
