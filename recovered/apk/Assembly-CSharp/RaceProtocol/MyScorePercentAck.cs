using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MyScorePercentAck : GeneratedMessageLite<MyScorePercentAck, MyScorePercentAck.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MyScorePercentAck, Builder>
	{
		private MyScorePercentAck result = new MyScorePercentAck();

		protected override Builder ThisBuilder => this;

		protected override MyScorePercentAck MessageBeingBuilt => result;

		public override MyScorePercentAck DefaultInstanceForType => DefaultInstance;

		public bool HasRankType => result.HasRankType;

		[CLSCompliant(false)]
		public uint RankType
		{
			get
			{
				return result.RankType;
			}
			set
			{
				SetRankType(value);
			}
		}

		public bool HasRankPercent => result.HasRankPercent;

		public float RankPercent
		{
			get
			{
				return result.RankPercent;
			}
			set
			{
				SetRankPercent(value);
			}
		}

		public bool HasMode => result.HasMode;

		[CLSCompliant(false)]
		public uint Mode
		{
			get
			{
				return result.Mode;
			}
			set
			{
				SetMode(value);
			}
		}

		public bool HasTrack => result.HasTrack;

		[CLSCompliant(false)]
		public uint Track
		{
			get
			{
				return result.Track;
			}
			set
			{
				SetTrack(value);
			}
		}

		public bool HasScore => result.HasScore;

		public MsgScore Score
		{
			get
			{
				return result.Score;
			}
			set
			{
				SetScore(value);
			}
		}

		public override Builder Clear()
		{
			result = new MyScorePercentAck();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MyScorePercentAck BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MyScorePercentAck myScorePercentAck = result;
			result = null;
			return myScorePercentAck;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MyScorePercentAck)
			{
				return MergeFrom((MyScorePercentAck)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MyScorePercentAck other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasRankType)
			{
				RankType = other.RankType;
			}
			if (other.HasRankPercent)
			{
				RankPercent = other.RankPercent;
			}
			if (other.HasMode)
			{
				Mode = other.Mode;
			}
			if (other.HasTrack)
			{
				Track = other.Track;
			}
			if (other.HasScore)
			{
				MergeScore(other.Score);
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
				case 8u:
					RankType = input.ReadUInt32();
					break;
				case 21u:
					RankPercent = input.ReadFloat();
					break;
				case 24u:
					Mode = input.ReadUInt32();
					break;
				case 32u:
					Track = input.ReadUInt32();
					break;
				case 42u:
				{
					MsgScore.Builder builder = MsgScore.CreateBuilder();
					if (HasScore)
					{
						builder.MergeFrom(Score);
					}
					input.ReadMessage(builder, extensionRegistry);
					Score = builder.BuildPartial();
					break;
				}
				}
			}
		}

		[CLSCompliant(false)]
		public Builder SetRankType(uint value)
		{
			result.hasRankType = true;
			result.rankType_ = value;
			return this;
		}

		public Builder ClearRankType()
		{
			result.hasRankType = false;
			result.rankType_ = 0u;
			return this;
		}

		public Builder SetRankPercent(float value)
		{
			result.hasRankPercent = true;
			result.rankPercent_ = value;
			return this;
		}

		public Builder ClearRankPercent()
		{
			result.hasRankPercent = false;
			result.rankPercent_ = 0f;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetMode(uint value)
		{
			result.hasMode = true;
			result.mode_ = value;
			return this;
		}

		public Builder ClearMode()
		{
			result.hasMode = false;
			result.mode_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetTrack(uint value)
		{
			result.hasTrack = true;
			result.track_ = value;
			return this;
		}

		public Builder ClearTrack()
		{
			result.hasTrack = false;
			result.track_ = 0u;
			return this;
		}

		public Builder SetScore(MsgScore value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasScore = true;
			result.score_ = value;
			return this;
		}

		public Builder SetScore(MsgScore.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.hasScore = true;
			result.score_ = builderForValue.Build();
			return this;
		}

		public Builder MergeScore(MsgScore value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			if (result.HasScore && result.score_ != MsgScore.DefaultInstance)
			{
				result.score_ = MsgScore.CreateBuilder(result.score_).MergeFrom(value).BuildPartial();
			}
			else
			{
				result.score_ = value;
			}
			result.hasScore = true;
			return this;
		}

		public Builder ClearScore()
		{
			result.hasScore = false;
			result.score_ = MsgScore.DefaultInstance;
			return this;
		}
	}

	public const int RankTypeFieldNumber = 1;

	public const int RankPercentFieldNumber = 2;

	public const int ModeFieldNumber = 3;

	public const int TrackFieldNumber = 4;

	public const int ScoreFieldNumber = 5;

	private static readonly MyScorePercentAck defaultInstance;

	private bool hasRankType;

	private uint rankType_;

	private bool hasRankPercent;

	private float rankPercent_;

	private bool hasMode;

	private uint mode_;

	private bool hasTrack;

	private uint track_;

	private bool hasScore;

	private MsgScore score_ = MsgScore.DefaultInstance;

	private int memoizedSerializedSize = -1;

	public static MyScorePercentAck DefaultInstance => defaultInstance;

	public override MyScorePercentAck DefaultInstanceForType => defaultInstance;

	protected override MyScorePercentAck ThisMessage => this;

	public bool HasRankType => hasRankType;

	[CLSCompliant(false)]
	public uint RankType => rankType_;

	public bool HasRankPercent => hasRankPercent;

	public float RankPercent => rankPercent_;

	public bool HasMode => hasMode;

	[CLSCompliant(false)]
	public uint Mode => mode_;

	public bool HasTrack => hasTrack;

	[CLSCompliant(false)]
	public uint Track => track_;

	public bool HasScore => hasScore;

	public MsgScore Score => score_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasRankType)
			{
				return false;
			}
			if (!hasRankPercent)
			{
				return false;
			}
			if (!hasMode)
			{
				return false;
			}
			if (!hasTrack)
			{
				return false;
			}
			if (!hasScore)
			{
				return false;
			}
			if (!Score.IsInitialized)
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
			if (HasRankType)
			{
				num += CodedOutputStream.ComputeUInt32Size(1, RankType);
			}
			if (HasRankPercent)
			{
				num += CodedOutputStream.ComputeFloatSize(2, RankPercent);
			}
			if (HasMode)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, Mode);
			}
			if (HasTrack)
			{
				num += CodedOutputStream.ComputeUInt32Size(4, Track);
			}
			if (HasScore)
			{
				num += CodedOutputStream.ComputeMessageSize(5, Score);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MyScorePercentAck()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasRankType)
		{
			output.WriteUInt32(1, RankType);
		}
		if (HasRankPercent)
		{
			output.WriteFloat(2, RankPercent);
		}
		if (HasMode)
		{
			output.WriteUInt32(3, Mode);
		}
		if (HasTrack)
		{
			output.WriteUInt32(4, Track);
		}
		if (HasScore)
		{
			output.WriteMessage(5, Score);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasRankType)
		{
			num ^= rankType_.GetHashCode();
		}
		if (hasRankPercent)
		{
			num ^= rankPercent_.GetHashCode();
		}
		if (hasMode)
		{
			num ^= mode_.GetHashCode();
		}
		if (hasTrack)
		{
			num ^= track_.GetHashCode();
		}
		if (hasScore)
		{
			num ^= score_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MyScorePercentAck myScorePercentAck))
		{
			return false;
		}
		if (hasRankType != myScorePercentAck.hasRankType || (hasRankType && !rankType_.Equals(myScorePercentAck.rankType_)))
		{
			return false;
		}
		if (hasRankPercent != myScorePercentAck.hasRankPercent || (hasRankPercent && !rankPercent_.Equals(myScorePercentAck.rankPercent_)))
		{
			return false;
		}
		if (hasMode != myScorePercentAck.hasMode || (hasMode && !mode_.Equals(myScorePercentAck.mode_)))
		{
			return false;
		}
		if (hasTrack != myScorePercentAck.hasTrack || (hasTrack && !track_.Equals(myScorePercentAck.track_)))
		{
			return false;
		}
		if (hasScore != myScorePercentAck.hasScore || (hasScore && !score_.Equals(myScorePercentAck.score_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MyScorePercentAck, Builder>.PrintField("rankType", hasRankType, rankType_, writer);
		GeneratedMessageLite<MyScorePercentAck, Builder>.PrintField("rankPercent", hasRankPercent, rankPercent_, writer);
		GeneratedMessageLite<MyScorePercentAck, Builder>.PrintField("mode", hasMode, mode_, writer);
		GeneratedMessageLite<MyScorePercentAck, Builder>.PrintField("track", hasTrack, track_, writer);
		GeneratedMessageLite<MyScorePercentAck, Builder>.PrintField("score", hasScore, score_, writer);
	}

	public static MyScorePercentAck ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentAck ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MyScorePercentAck ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MyScorePercentAck ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MyScorePercentAck prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
