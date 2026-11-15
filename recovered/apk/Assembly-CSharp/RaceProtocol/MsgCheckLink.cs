using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgCheckLink : GeneratedMessageLite<MsgCheckLink, MsgCheckLink.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgCheckLink, Builder>
	{
		private MsgCheckLink result = new MsgCheckLink();

		protected override Builder ThisBuilder => this;

		protected override MsgCheckLink MessageBeingBuilt => result;

		public override MsgCheckLink DefaultInstanceForType => DefaultInstance;

		public bool HasReceived => result.HasReceived;

		public int Received
		{
			get
			{
				return result.Received;
			}
			set
			{
				SetReceived(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgCheckLink();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgCheckLink BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgCheckLink msgCheckLink = result;
			result = null;
			return msgCheckLink;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgCheckLink)
			{
				return MergeFrom((MsgCheckLink)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgCheckLink other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasReceived)
			{
				Received = other.Received;
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
				switch (num)
				{
				case 0u:
					return this;
				case 8u:
					Received = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetReceived(int value)
		{
			result.hasReceived = true;
			result.received_ = value;
			return this;
		}

		public Builder ClearReceived()
		{
			result.hasReceived = false;
			result.received_ = 0;
			return this;
		}
	}

	public const int ReceivedFieldNumber = 1;

	private static readonly MsgCheckLink defaultInstance;

	private bool hasReceived;

	private int received_;

	private int memoizedSerializedSize = -1;

	public static MsgCheckLink DefaultInstance => defaultInstance;

	public override MsgCheckLink DefaultInstanceForType => defaultInstance;

	protected override MsgCheckLink ThisMessage => this;

	public bool HasReceived => hasReceived;

	public int Received => received_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasReceived)
			{
				return false;
			}
			return true;
		}
	}

	public override int SerializedSize
	{
		get
		{
			int num = memoizedSerializedSize;
			if (num != -1)
			{
				return num;
			}
			num = 0;
			if (HasReceived)
			{
				num += CodedOutputStream.ComputeInt32Size(1, Received);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgCheckLink()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasReceived)
		{
			output.WriteInt32(1, Received);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasReceived)
		{
			num ^= received_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgCheckLink msgCheckLink))
		{
			return false;
		}
		if (hasReceived != msgCheckLink.hasReceived || (hasReceived && !received_.Equals(msgCheckLink.received_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgCheckLink, Builder>.PrintField("received", hasReceived, received_, writer);
	}

	public static MsgCheckLink ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgCheckLink ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgCheckLink ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgCheckLink ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgCheckLink prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
