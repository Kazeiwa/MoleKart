using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPlayerResult : GeneratedMessageLite<MsgPlayerResult, MsgPlayerResult.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPlayerResult, Builder>
	{
		private MsgPlayerResult result = new MsgPlayerResult();

		protected override Builder ThisBuilder => this;

		protected override MsgPlayerResult MessageBeingBuilt => result;

		public override MsgPlayerResult DefaultInstanceForType => DefaultInstance;

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

		public bool HasTime => result.HasTime;

		public float Time
		{
			get
			{
				return result.Time;
			}
			set
			{
				SetTime(value);
			}
		}

		public bool HasRanking => result.HasRanking;

		public int Ranking
		{
			get
			{
				return result.Ranking;
			}
			set
			{
				SetRanking(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgPlayerResult();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPlayerResult BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPlayerResult msgPlayerResult = result;
			result = null;
			return msgPlayerResult;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPlayerResult)
			{
				return MergeFrom((MsgPlayerResult)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPlayerResult other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPlayerId)
			{
				PlayerId = other.PlayerId;
			}
			if (other.HasTime)
			{
				Time = other.Time;
			}
			if (other.HasRanking)
			{
				Ranking = other.Ranking;
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
				case 21u:
					Time = input.ReadFloat();
					continue;
				case 24u:
					Ranking = input.ReadInt32();
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

		public Builder SetTime(float value)
		{
			result.hasTime = true;
			result.time_ = value;
			return this;
		}

		public Builder ClearTime()
		{
			result.hasTime = false;
			result.time_ = 0f;
			return this;
		}

		public Builder SetRanking(int value)
		{
			result.hasRanking = true;
			result.ranking_ = value;
			return this;
		}

		public Builder ClearRanking()
		{
			result.hasRanking = false;
			result.ranking_ = 0;
			return this;
		}
	}

	public const int PlayerIdFieldNumber = 1;

	public const int TimeFieldNumber = 2;

	public const int RankingFieldNumber = 3;

	private static readonly MsgPlayerResult defaultInstance;

	private bool hasPlayerId;

	private int playerId_;

	private bool hasTime;

	private float time_;

	private bool hasRanking;

	private int ranking_;

	private int memoizedSerializedSize = -1;

	public static MsgPlayerResult DefaultInstance => defaultInstance;

	public override MsgPlayerResult DefaultInstanceForType => defaultInstance;

	protected override MsgPlayerResult ThisMessage => this;

	public bool HasPlayerId => hasPlayerId;

	public int PlayerId => playerId_;

	public bool HasTime => hasTime;

	public float Time => time_;

	public bool HasRanking => hasRanking;

	public int Ranking => ranking_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPlayerId)
			{
				return false;
			}
			if (!hasTime)
			{
				return false;
			}
			if (!hasRanking)
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
			if (HasTime)
			{
				num += CodedOutputStream.ComputeFloatSize(2, Time);
			}
			if (HasRanking)
			{
				num += CodedOutputStream.ComputeInt32Size(3, Ranking);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPlayerResult()
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
		if (HasTime)
		{
			output.WriteFloat(2, Time);
		}
		if (HasRanking)
		{
			output.WriteInt32(3, Ranking);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPlayerId)
		{
			num ^= playerId_.GetHashCode();
		}
		if (hasTime)
		{
			num ^= time_.GetHashCode();
		}
		if (hasRanking)
		{
			num ^= ranking_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPlayerResult msgPlayerResult))
		{
			return false;
		}
		if (hasPlayerId != msgPlayerResult.hasPlayerId || (hasPlayerId && !playerId_.Equals(msgPlayerResult.playerId_)))
		{
			return false;
		}
		if (hasTime != msgPlayerResult.hasTime || (hasTime && !time_.Equals(msgPlayerResult.time_)))
		{
			return false;
		}
		if (hasRanking != msgPlayerResult.hasRanking || (hasRanking && !ranking_.Equals(msgPlayerResult.ranking_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPlayerResult, Builder>.PrintField("playerId", hasPlayerId, playerId_, writer);
		GeneratedMessageLite<MsgPlayerResult, Builder>.PrintField("time", hasTime, time_, writer);
		GeneratedMessageLite<MsgPlayerResult, Builder>.PrintField("ranking", hasRanking, ranking_, writer);
	}

	public static MsgPlayerResult ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerResult ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPlayerResult ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerResult ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPlayerResult prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
