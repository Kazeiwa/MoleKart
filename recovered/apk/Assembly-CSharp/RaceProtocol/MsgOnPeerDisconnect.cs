using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgOnPeerDisconnect : GeneratedMessageLite<MsgOnPeerDisconnect, MsgOnPeerDisconnect.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgOnPeerDisconnect, Builder>
	{
		private MsgOnPeerDisconnect result = new MsgOnPeerDisconnect();

		protected override Builder ThisBuilder => this;

		protected override MsgOnPeerDisconnect MessageBeingBuilt => result;

		public override MsgOnPeerDisconnect DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgOnPeerDisconnect();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgOnPeerDisconnect BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgOnPeerDisconnect msgOnPeerDisconnect = result;
			result = null;
			return msgOnPeerDisconnect;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgOnPeerDisconnect)
			{
				return MergeFrom((MsgOnPeerDisconnect)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgOnPeerDisconnect other)
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

	private static readonly MsgOnPeerDisconnect defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgOnPeerDisconnect DefaultInstance => defaultInstance;

	public override MsgOnPeerDisconnect DefaultInstanceForType => defaultInstance;

	protected override MsgOnPeerDisconnect ThisMessage => this;

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

	static MsgOnPeerDisconnect()
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
		if (!(obj is MsgOnPeerDisconnect))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgOnPeerDisconnect ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgOnPeerDisconnect ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgOnPeerDisconnect prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
