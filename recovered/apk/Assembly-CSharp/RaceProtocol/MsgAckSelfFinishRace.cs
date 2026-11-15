using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgAckSelfFinishRace : GeneratedMessageLite<MsgAckSelfFinishRace, MsgAckSelfFinishRace.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgAckSelfFinishRace, Builder>
	{
		private MsgAckSelfFinishRace result = new MsgAckSelfFinishRace();

		protected override Builder ThisBuilder => this;

		protected override MsgAckSelfFinishRace MessageBeingBuilt => result;

		public override MsgAckSelfFinishRace DefaultInstanceForType => DefaultInstance;

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
			result = new MsgAckSelfFinishRace();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgAckSelfFinishRace BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgAckSelfFinishRace msgAckSelfFinishRace = result;
			result = null;
			return msgAckSelfFinishRace;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgAckSelfFinishRace)
			{
				return MergeFrom((MsgAckSelfFinishRace)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgAckSelfFinishRace other)
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

	private static readonly MsgAckSelfFinishRace defaultInstance;

	private bool hasReceived;

	private int received_;

	private int memoizedSerializedSize = -1;

	public static MsgAckSelfFinishRace DefaultInstance => defaultInstance;

	public override MsgAckSelfFinishRace DefaultInstanceForType => defaultInstance;

	protected override MsgAckSelfFinishRace ThisMessage => this;

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

	static MsgAckSelfFinishRace()
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
		if (!(obj is MsgAckSelfFinishRace msgAckSelfFinishRace))
		{
			return false;
		}
		if (hasReceived != msgAckSelfFinishRace.hasReceived || (hasReceived && !received_.Equals(msgAckSelfFinishRace.received_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgAckSelfFinishRace, Builder>.PrintField("received", hasReceived, received_, writer);
	}

	public static MsgAckSelfFinishRace ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgAckSelfFinishRace ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgAckSelfFinishRace prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
