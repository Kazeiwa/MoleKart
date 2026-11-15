using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgRequestPlayerList : GeneratedMessageLite<MsgRequestPlayerList, MsgRequestPlayerList.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgRequestPlayerList, Builder>
	{
		private MsgRequestPlayerList result = new MsgRequestPlayerList();

		protected override Builder ThisBuilder => this;

		protected override MsgRequestPlayerList MessageBeingBuilt => result;

		public override MsgRequestPlayerList DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgRequestPlayerList();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgRequestPlayerList BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgRequestPlayerList msgRequestPlayerList = result;
			result = null;
			return msgRequestPlayerList;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgRequestPlayerList)
			{
				return MergeFrom((MsgRequestPlayerList)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgRequestPlayerList other)
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

	private static readonly MsgRequestPlayerList defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgRequestPlayerList DefaultInstance => defaultInstance;

	public override MsgRequestPlayerList DefaultInstanceForType => defaultInstance;

	protected override MsgRequestPlayerList ThisMessage => this;

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

	static MsgRequestPlayerList()
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
		if (!(obj is MsgRequestPlayerList))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgRequestPlayerList ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgRequestPlayerList ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgRequestPlayerList ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgRequestPlayerList ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgRequestPlayerList prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
