using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgIntoBackground : GeneratedMessageLite<MsgIntoBackground, MsgIntoBackground.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgIntoBackground, Builder>
	{
		private MsgIntoBackground result = new MsgIntoBackground();

		protected override Builder ThisBuilder => this;

		protected override MsgIntoBackground MessageBeingBuilt => result;

		public override MsgIntoBackground DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgIntoBackground();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgIntoBackground BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgIntoBackground msgIntoBackground = result;
			result = null;
			return msgIntoBackground;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgIntoBackground)
			{
				return MergeFrom((MsgIntoBackground)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgIntoBackground other)
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

	private static readonly MsgIntoBackground defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgIntoBackground DefaultInstance => defaultInstance;

	public override MsgIntoBackground DefaultInstanceForType => defaultInstance;

	protected override MsgIntoBackground ThisMessage => this;

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

	static MsgIntoBackground()
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
		if (!(obj is MsgIntoBackground))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgIntoBackground ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgIntoBackground ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgIntoBackground ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgIntoBackground ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgIntoBackground prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
