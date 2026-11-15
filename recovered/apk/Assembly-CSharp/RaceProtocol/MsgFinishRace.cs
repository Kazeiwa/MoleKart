using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgFinishRace : GeneratedMessageLite<MsgFinishRace, MsgFinishRace.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgFinishRace, Builder>
	{
		private MsgFinishRace result = new MsgFinishRace();

		protected override Builder ThisBuilder => this;

		protected override MsgFinishRace MessageBeingBuilt => result;

		public override MsgFinishRace DefaultInstanceForType => DefaultInstance;

		public bool HasPlayerId => result.HasPlayerId;

		public int PlayerId
		{
			get
			{
				return result.PlayerId;
			}
			set
			{
				SetPlayerId(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgFinishRace();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgFinishRace BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgFinishRace msgFinishRace = result;
			result = null;
			return msgFinishRace;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgFinishRace)
			{
				return MergeFrom((MsgFinishRace)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgFinishRace other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPlayerId)
			{
				PlayerId = other.PlayerId;
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
					PlayerId = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPlayerId(int value)
		{
			result.hasPlayerId = true;
			result.playerId_ = value;
			return this;
		}

		public Builder ClearPlayerId()
		{
			result.hasPlayerId = false;
			result.playerId_ = 0;
			return this;
		}
	}

	public const int PlayerIdFieldNumber = 1;

	private static readonly MsgFinishRace defaultInstance;

	private bool hasPlayerId;

	private int playerId_;

	private int memoizedSerializedSize = -1;

	public static MsgFinishRace DefaultInstance => defaultInstance;

	public override MsgFinishRace DefaultInstanceForType => defaultInstance;

	protected override MsgFinishRace ThisMessage => this;

	public bool HasPlayerId => hasPlayerId;

	public int PlayerId => playerId_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPlayerId)
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
			if (HasPlayerId)
			{
				num += CodedOutputStream.ComputeInt32Size(1, PlayerId);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgFinishRace()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPlayerId)
		{
			output.WriteInt32(1, PlayerId);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPlayerId)
		{
			num ^= playerId_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgFinishRace msgFinishRace))
		{
			return false;
		}
		if (hasPlayerId != msgFinishRace.hasPlayerId || (hasPlayerId && !playerId_.Equals(msgFinishRace.playerId_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgFinishRace, Builder>.PrintField("playerId", hasPlayerId, playerId_, writer);
	}

	public static MsgFinishRace ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRace ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgFinishRace ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgFinishRace ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgFinishRace prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
