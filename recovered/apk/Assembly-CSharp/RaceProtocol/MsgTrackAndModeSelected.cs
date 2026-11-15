using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgTrackAndModeSelected : GeneratedMessageLite<MsgTrackAndModeSelected, MsgTrackAndModeSelected.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgTrackAndModeSelected, Builder>
	{
		private MsgTrackAndModeSelected result = new MsgTrackAndModeSelected();

		protected override Builder ThisBuilder => this;

		protected override MsgTrackAndModeSelected MessageBeingBuilt => result;

		public override MsgTrackAndModeSelected DefaultInstanceForType => DefaultInstance;

		public bool HasTrackId => result.HasTrackId;

		public int TrackId
		{
			get
			{
				return result.TrackId;
			}
			set
			{
				SetTrackId(value);
			}
		}

		public bool HasGameMode => result.HasGameMode;

		public int GameMode
		{
			get
			{
				return result.GameMode;
			}
			set
			{
				SetGameMode(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgTrackAndModeSelected();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgTrackAndModeSelected BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgTrackAndModeSelected msgTrackAndModeSelected = result;
			result = null;
			return msgTrackAndModeSelected;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgTrackAndModeSelected)
			{
				return MergeFrom((MsgTrackAndModeSelected)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgTrackAndModeSelected other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasTrackId)
			{
				TrackId = other.TrackId;
			}
			if (other.HasGameMode)
			{
				GameMode = other.GameMode;
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
					TrackId = input.ReadInt32();
					continue;
				case 16u:
					GameMode = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetTrackId(int value)
		{
			result.hasTrackId = true;
			result.trackId_ = value;
			return this;
		}

		public Builder ClearTrackId()
		{
			result.hasTrackId = false;
			result.trackId_ = 0;
			return this;
		}

		public Builder SetGameMode(int value)
		{
			result.hasGameMode = true;
			result.gameMode_ = value;
			return this;
		}

		public Builder ClearGameMode()
		{
			result.hasGameMode = false;
			result.gameMode_ = 0;
			return this;
		}
	}

	public const int TrackIdFieldNumber = 1;

	public const int GameModeFieldNumber = 2;

	private static readonly MsgTrackAndModeSelected defaultInstance;

	private bool hasTrackId;

	private int trackId_;

	private bool hasGameMode;

	private int gameMode_;

	private int memoizedSerializedSize = -1;

	public static MsgTrackAndModeSelected DefaultInstance => defaultInstance;

	public override MsgTrackAndModeSelected DefaultInstanceForType => defaultInstance;

	protected override MsgTrackAndModeSelected ThisMessage => this;

	public bool HasTrackId => hasTrackId;

	public int TrackId => trackId_;

	public bool HasGameMode => hasGameMode;

	public int GameMode => gameMode_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasTrackId)
			{
				return false;
			}
			if (!hasGameMode)
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
			if (HasTrackId)
			{
				num += CodedOutputStream.ComputeInt32Size(1, TrackId);
			}
			if (HasGameMode)
			{
				num += CodedOutputStream.ComputeInt32Size(2, GameMode);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgTrackAndModeSelected()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasTrackId)
		{
			output.WriteInt32(1, TrackId);
		}
		if (HasGameMode)
		{
			output.WriteInt32(2, GameMode);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasTrackId)
		{
			num ^= trackId_.GetHashCode();
		}
		if (hasGameMode)
		{
			num ^= gameMode_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgTrackAndModeSelected msgTrackAndModeSelected))
		{
			return false;
		}
		if (hasTrackId != msgTrackAndModeSelected.hasTrackId || (hasTrackId && !trackId_.Equals(msgTrackAndModeSelected.trackId_)))
		{
			return false;
		}
		if (hasGameMode != msgTrackAndModeSelected.hasGameMode || (hasGameMode && !gameMode_.Equals(msgTrackAndModeSelected.gameMode_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgTrackAndModeSelected, Builder>.PrintField("trackId", hasTrackId, trackId_, writer);
		GeneratedMessageLite<MsgTrackAndModeSelected, Builder>.PrintField("gameMode", hasGameMode, gameMode_, writer);
	}

	public static MsgTrackAndModeSelected ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgTrackAndModeSelected ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgTrackAndModeSelected prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
