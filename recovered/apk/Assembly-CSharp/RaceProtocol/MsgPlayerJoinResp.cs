using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPlayerJoinResp : GeneratedMessageLite<MsgPlayerJoinResp, MsgPlayerJoinResp.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPlayerJoinResp, Builder>
	{
		private MsgPlayerJoinResp result = new MsgPlayerJoinResp();

		protected override Builder ThisBuilder => this;

		protected override MsgPlayerJoinResp MessageBeingBuilt => result;

		public override MsgPlayerJoinResp DefaultInstanceForType => DefaultInstance;

		public bool HasPeerId => result.HasPeerId;

		public string PeerId
		{
			get
			{
				return result.PeerId;
			}
			set
			{
				SetPeerId(value);
			}
		}

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

		public bool HasStartPosition => result.HasStartPosition;

		public int StartPosition
		{
			get
			{
				return result.StartPosition;
			}
			set
			{
				SetStartPosition(value);
			}
		}

		public bool HasResult => result.HasResult;

		public int Result
		{
			get
			{
				return result.Result;
			}
			set
			{
				SetResult(value);
			}
		}

		public bool HasTrackMode => result.HasTrackMode;

		public MsgTrackAndModeSelected TrackMode
		{
			get
			{
				return result.TrackMode;
			}
			set
			{
				SetTrackMode(value);
			}
		}

		public bool HasPlayerlist => result.HasPlayerlist;

		public MsgNotifyPlayerList Playerlist
		{
			get
			{
				return result.Playerlist;
			}
			set
			{
				SetPlayerlist(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgPlayerJoinResp();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPlayerJoinResp BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPlayerJoinResp msgPlayerJoinResp = result;
			result = null;
			return msgPlayerJoinResp;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPlayerJoinResp)
			{
				return MergeFrom((MsgPlayerJoinResp)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPlayerJoinResp other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPeerId)
			{
				PeerId = other.PeerId;
			}
			if (other.HasPlayerId)
			{
				PlayerId = other.PlayerId;
			}
			if (other.HasStartPosition)
			{
				StartPosition = other.StartPosition;
			}
			if (other.HasResult)
			{
				Result = other.Result;
			}
			if (other.HasTrackMode)
			{
				MergeTrackMode(other.TrackMode);
			}
			if (other.HasPlayerlist)
			{
				MergePlayerlist(other.Playerlist);
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
				default:
					if (WireFormat.IsEndGroupTag(num))
					{
						return this;
					}
					ParseUnknownField(input, extensionRegistry, num);
					break;
				case 10u:
					PeerId = input.ReadString();
					break;
				case 16u:
					PlayerId = input.ReadInt32();
					break;
				case 24u:
					StartPosition = input.ReadInt32();
					break;
				case 32u:
					Result = input.ReadInt32();
					break;
				case 42u:
				{
					MsgTrackAndModeSelected.Builder builder2 = MsgTrackAndModeSelected.CreateBuilder();
					if (HasTrackMode)
					{
						builder2.MergeFrom(TrackMode);
					}
					input.ReadMessage(builder2, extensionRegistry);
					TrackMode = builder2.BuildPartial();
					break;
				}
				case 50u:
				{
					MsgNotifyPlayerList.Builder builder = MsgNotifyPlayerList.CreateBuilder();
					if (HasPlayerlist)
					{
						builder.MergeFrom(Playerlist);
					}
					input.ReadMessage(builder, extensionRegistry);
					Playerlist = builder.BuildPartial();
					break;
				}
				}
			}
		}

		public Builder SetPeerId(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasPeerId = true;
			result.peerId_ = value;
			return this;
		}

		public Builder ClearPeerId()
		{
			result.hasPeerId = false;
			result.peerId_ = string.Empty;
			return this;
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

		public Builder SetStartPosition(int value)
		{
			result.hasStartPosition = true;
			result.startPosition_ = value;
			return this;
		}

		public Builder ClearStartPosition()
		{
			result.hasStartPosition = false;
			result.startPosition_ = 0;
			return this;
		}

		public Builder SetResult(int value)
		{
			result.hasResult = true;
			result.result_ = value;
			return this;
		}

		public Builder ClearResult()
		{
			result.hasResult = false;
			result.result_ = 0;
			return this;
		}

		public Builder SetTrackMode(MsgTrackAndModeSelected value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasTrackMode = true;
			result.trackMode_ = value;
			return this;
		}

		public Builder SetTrackMode(MsgTrackAndModeSelected.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.hasTrackMode = true;
			result.trackMode_ = builderForValue.Build();
			return this;
		}

		public Builder MergeTrackMode(MsgTrackAndModeSelected value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			if (result.HasTrackMode && result.trackMode_ != MsgTrackAndModeSelected.DefaultInstance)
			{
				result.trackMode_ = MsgTrackAndModeSelected.CreateBuilder(result.trackMode_).MergeFrom(value).BuildPartial();
			}
			else
			{
				result.trackMode_ = value;
			}
			result.hasTrackMode = true;
			return this;
		}

		public Builder ClearTrackMode()
		{
			result.hasTrackMode = false;
			result.trackMode_ = MsgTrackAndModeSelected.DefaultInstance;
			return this;
		}

		public Builder SetPlayerlist(MsgNotifyPlayerList value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasPlayerlist = true;
			result.playerlist_ = value;
			return this;
		}

		public Builder SetPlayerlist(MsgNotifyPlayerList.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.hasPlayerlist = true;
			result.playerlist_ = builderForValue.Build();
			return this;
		}

		public Builder MergePlayerlist(MsgNotifyPlayerList value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			if (result.HasPlayerlist && result.playerlist_ != MsgNotifyPlayerList.DefaultInstance)
			{
				result.playerlist_ = MsgNotifyPlayerList.CreateBuilder(result.playerlist_).MergeFrom(value).BuildPartial();
			}
			else
			{
				result.playerlist_ = value;
			}
			result.hasPlayerlist = true;
			return this;
		}

		public Builder ClearPlayerlist()
		{
			result.hasPlayerlist = false;
			result.playerlist_ = MsgNotifyPlayerList.DefaultInstance;
			return this;
		}
	}

	public const int PeerIdFieldNumber = 1;

	public const int PlayerIdFieldNumber = 2;

	public const int StartPositionFieldNumber = 3;

	public const int ResultFieldNumber = 4;

	public const int TrackModeFieldNumber = 5;

	public const int PlayerlistFieldNumber = 6;

	private static readonly MsgPlayerJoinResp defaultInstance;

	private bool hasPeerId;

	private string peerId_ = string.Empty;

	private bool hasPlayerId;

	private int playerId_;

	private bool hasStartPosition;

	private int startPosition_;

	private bool hasResult;

	private int result_;

	private bool hasTrackMode;

	private MsgTrackAndModeSelected trackMode_ = MsgTrackAndModeSelected.DefaultInstance;

	private bool hasPlayerlist;

	private MsgNotifyPlayerList playerlist_ = MsgNotifyPlayerList.DefaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgPlayerJoinResp DefaultInstance => defaultInstance;

	public override MsgPlayerJoinResp DefaultInstanceForType => defaultInstance;

	protected override MsgPlayerJoinResp ThisMessage => this;

	public bool HasPeerId => hasPeerId;

	public string PeerId => peerId_;

	public bool HasPlayerId => hasPlayerId;

	public int PlayerId => playerId_;

	public bool HasStartPosition => hasStartPosition;

	public int StartPosition => startPosition_;

	public bool HasResult => hasResult;

	public int Result => result_;

	public bool HasTrackMode => hasTrackMode;

	public MsgTrackAndModeSelected TrackMode => trackMode_;

	public bool HasPlayerlist => hasPlayerlist;

	public MsgNotifyPlayerList Playerlist => playerlist_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPeerId)
			{
				return false;
			}
			if (!hasPlayerId)
			{
				return false;
			}
			if (!hasStartPosition)
			{
				return false;
			}
			if (!hasResult)
			{
				return false;
			}
			if (HasTrackMode && !TrackMode.IsInitialized)
			{
				return false;
			}
			if (HasPlayerlist && !Playerlist.IsInitialized)
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
			if (HasPeerId)
			{
				num += CodedOutputStream.ComputeStringSize(1, PeerId);
			}
			if (HasPlayerId)
			{
				num += CodedOutputStream.ComputeInt32Size(2, PlayerId);
			}
			if (HasStartPosition)
			{
				num += CodedOutputStream.ComputeInt32Size(3, StartPosition);
			}
			if (HasResult)
			{
				num += CodedOutputStream.ComputeInt32Size(4, Result);
			}
			if (HasTrackMode)
			{
				num += CodedOutputStream.ComputeMessageSize(5, TrackMode);
			}
			if (HasPlayerlist)
			{
				num += CodedOutputStream.ComputeMessageSize(6, Playerlist);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPlayerJoinResp()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPeerId)
		{
			output.WriteString(1, PeerId);
		}
		if (HasPlayerId)
		{
			output.WriteInt32(2, PlayerId);
		}
		if (HasStartPosition)
		{
			output.WriteInt32(3, StartPosition);
		}
		if (HasResult)
		{
			output.WriteInt32(4, Result);
		}
		if (HasTrackMode)
		{
			output.WriteMessage(5, TrackMode);
		}
		if (HasPlayerlist)
		{
			output.WriteMessage(6, Playerlist);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPeerId)
		{
			num ^= peerId_.GetHashCode();
		}
		if (hasPlayerId)
		{
			num ^= playerId_.GetHashCode();
		}
		if (hasStartPosition)
		{
			num ^= startPosition_.GetHashCode();
		}
		if (hasResult)
		{
			num ^= result_.GetHashCode();
		}
		if (hasTrackMode)
		{
			num ^= trackMode_.GetHashCode();
		}
		if (hasPlayerlist)
		{
			num ^= playerlist_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPlayerJoinResp msgPlayerJoinResp))
		{
			return false;
		}
		if (hasPeerId != msgPlayerJoinResp.hasPeerId || (hasPeerId && !peerId_.Equals(msgPlayerJoinResp.peerId_)))
		{
			return false;
		}
		if (hasPlayerId != msgPlayerJoinResp.hasPlayerId || (hasPlayerId && !playerId_.Equals(msgPlayerJoinResp.playerId_)))
		{
			return false;
		}
		if (hasStartPosition != msgPlayerJoinResp.hasStartPosition || (hasStartPosition && !startPosition_.Equals(msgPlayerJoinResp.startPosition_)))
		{
			return false;
		}
		if (hasResult != msgPlayerJoinResp.hasResult || (hasResult && !result_.Equals(msgPlayerJoinResp.result_)))
		{
			return false;
		}
		if (hasTrackMode != msgPlayerJoinResp.hasTrackMode || (hasTrackMode && !trackMode_.Equals(msgPlayerJoinResp.trackMode_)))
		{
			return false;
		}
		if (hasPlayerlist != msgPlayerJoinResp.hasPlayerlist || (hasPlayerlist && !playerlist_.Equals(msgPlayerJoinResp.playerlist_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPlayerJoinResp, Builder>.PrintField("peerId", hasPeerId, peerId_, writer);
		GeneratedMessageLite<MsgPlayerJoinResp, Builder>.PrintField("playerId", hasPlayerId, playerId_, writer);
		GeneratedMessageLite<MsgPlayerJoinResp, Builder>.PrintField("startPosition", hasStartPosition, startPosition_, writer);
		GeneratedMessageLite<MsgPlayerJoinResp, Builder>.PrintField("result", hasResult, result_, writer);
		GeneratedMessageLite<MsgPlayerJoinResp, Builder>.PrintField("TrackMode", hasTrackMode, trackMode_, writer);
		GeneratedMessageLite<MsgPlayerJoinResp, Builder>.PrintField("playerlist", hasPlayerlist, playerlist_, writer);
	}

	public static MsgPlayerJoinResp ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerJoinResp ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPlayerJoinResp prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
