using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgHostLeave : GeneratedMessageLite<MsgHostLeave, MsgHostLeave.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgHostLeave, Builder>
	{
		private MsgHostLeave result = new MsgHostLeave();

		protected override Builder ThisBuilder => this;

		protected override MsgHostLeave MessageBeingBuilt => result;

		public override MsgHostLeave DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgHostLeave();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgHostLeave BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgHostLeave msgHostLeave = result;
			result = null;
			return msgHostLeave;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgHostLeave)
			{
				return MergeFrom((MsgHostLeave)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgHostLeave other)
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

	private static readonly MsgHostLeave defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgHostLeave DefaultInstance => defaultInstance;

	public override MsgHostLeave DefaultInstanceForType => defaultInstance;

	protected override MsgHostLeave ThisMessage => this;

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

	static MsgHostLeave()
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
		if (!(obj is MsgHostLeave))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgHostLeave ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgHostLeave ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgHostLeave ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgHostLeave ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgHostLeave prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
