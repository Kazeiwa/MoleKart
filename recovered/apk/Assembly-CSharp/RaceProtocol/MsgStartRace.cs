using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgStartRace : GeneratedMessageLite<MsgStartRace, MsgStartRace.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgStartRace, Builder>
	{
		private MsgStartRace result = new MsgStartRace();

		protected override Builder ThisBuilder => this;

		protected override MsgStartRace MessageBeingBuilt => result;

		public override MsgStartRace DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgStartRace();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgStartRace BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgStartRace msgStartRace = result;
			result = null;
			return msgStartRace;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgStartRace)
			{
				return MergeFrom((MsgStartRace)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgStartRace other)
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

	private static readonly MsgStartRace defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgStartRace DefaultInstance => defaultInstance;

	public override MsgStartRace DefaultInstanceForType => defaultInstance;

	protected override MsgStartRace ThisMessage => this;

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

	static MsgStartRace()
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
		if (!(obj is MsgStartRace))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgStartRace ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgStartRace ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgStartRace ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgStartRace ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgStartRace ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgStartRace ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgStartRace ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgStartRace ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgStartRace ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgStartRace ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgStartRace prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
