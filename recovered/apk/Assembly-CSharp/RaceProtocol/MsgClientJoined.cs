using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgClientJoined : GeneratedMessageLite<MsgClientJoined, MsgClientJoined.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgClientJoined, Builder>
	{
		private MsgClientJoined result = new MsgClientJoined();

		protected override Builder ThisBuilder => this;

		protected override MsgClientJoined MessageBeingBuilt => result;

		public override MsgClientJoined DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgClientJoined();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgClientJoined BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgClientJoined msgClientJoined = result;
			result = null;
			return msgClientJoined;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgClientJoined)
			{
				return MergeFrom((MsgClientJoined)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgClientJoined other)
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

	private static readonly MsgClientJoined defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgClientJoined DefaultInstance => defaultInstance;

	public override MsgClientJoined DefaultInstanceForType => defaultInstance;

	protected override MsgClientJoined ThisMessage => this;

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

	static MsgClientJoined()
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
		if (!(obj is MsgClientJoined))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgClientJoined ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgClientJoined ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgClientJoined ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgClientJoined ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgClientJoined prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
