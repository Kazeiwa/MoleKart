using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgScore : GeneratedMessageLite<MsgScore, MsgScore.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgScore, Builder>
	{
		private MsgScore result = new MsgScore();

		protected override Builder ThisBuilder => this;

		protected override MsgScore MessageBeingBuilt => result;

		public override MsgScore DefaultInstanceForType => DefaultInstance;

		public bool HasRankNo => result.HasRankNo;

		public int RankNo
		{
			get
			{
				return result.RankNo;
			}
			set
			{
				SetRankNo(value);
			}
		}

		public bool HasUserId => result.HasUserId;

		[CLSCompliant(false)]
		public uint UserId
		{
			get
			{
				return result.UserId;
			}
			set
			{
				SetUserId(value);
			}
		}

		public bool HasNickName => result.HasNickName;

		public string NickName
		{
			get
			{
				return result.NickName;
			}
			set
			{
				SetNickName(value);
			}
		}

		public bool HasScore => result.HasScore;

		[CLSCompliant(false)]
		public uint Score
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
			result = new MsgScore();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgScore BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgScore msgScore = result;
			result = null;
			return msgScore;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgScore)
			{
				return MergeFrom((MsgScore)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgScore other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasRankNo)
			{
				RankNo = other.RankNo;
			}
			if (other.HasUserId)
			{
				UserId = other.UserId;
			}
			if (other.HasNickName)
			{
				NickName = other.NickName;
			}
			if (other.HasScore)
			{
				Score = other.Score;
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
					RankNo = input.ReadInt32();
					continue;
				case 16u:
					UserId = input.ReadUInt32();
					continue;
				case 26u:
					NickName = input.ReadString();
					continue;
				case 32u:
					Score = input.ReadUInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetRankNo(int value)
		{
			result.hasRankNo = true;
			result.rankNo_ = value;
			return this;
		}

		public Builder ClearRankNo()
		{
			result.hasRankNo = false;
			result.rankNo_ = 0;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetUserId(uint value)
		{
			result.hasUserId = true;
			result.userId_ = value;
			return this;
		}

		public Builder ClearUserId()
		{
			result.hasUserId = false;
			result.userId_ = 0u;
			return this;
		}

		public Builder SetNickName(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasNickName = true;
			result.nickName_ = value;
			return this;
		}

		public Builder ClearNickName()
		{
			result.hasNickName = false;
			result.nickName_ = string.Empty;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetScore(uint value)
		{
			result.hasScore = true;
			result.score_ = value;
			return this;
		}

		public Builder ClearScore()
		{
			result.hasScore = false;
			result.score_ = 0u;
			return this;
		}
	}

	public const int RankNoFieldNumber = 1;

	public const int UserIdFieldNumber = 2;

	public const int NickNameFieldNumber = 3;

	public const int ScoreFieldNumber = 4;

	private static readonly MsgScore defaultInstance;

	private bool hasRankNo;

	private int rankNo_;

	private bool hasUserId;

	private uint userId_;

	private bool hasNickName;

	private string nickName_ = string.Empty;

	private bool hasScore;

	private uint score_;

	private int memoizedSerializedSize = -1;

	public static MsgScore DefaultInstance => defaultInstance;

	public override MsgScore DefaultInstanceForType => defaultInstance;

	protected override MsgScore ThisMessage => this;

	public bool HasRankNo => hasRankNo;

	public int RankNo => rankNo_;

	public bool HasUserId => hasUserId;

	[CLSCompliant(false)]
	public uint UserId => userId_;

	public bool HasNickName => hasNickName;

	public string NickName => nickName_;

	public bool HasScore => hasScore;

	[CLSCompliant(false)]
	public uint Score => score_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasRankNo)
			{
				return false;
			}
			if (!hasUserId)
			{
				return false;
			}
			if (!hasNickName)
			{
				return false;
			}
			if (!hasScore)
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
			if (HasRankNo)
			{
				num += CodedOutputStream.ComputeInt32Size(1, RankNo);
			}
			if (HasUserId)
			{
				num += CodedOutputStream.ComputeUInt32Size(2, UserId);
			}
			if (HasNickName)
			{
				num += CodedOutputStream.ComputeStringSize(3, NickName);
			}
			if (HasScore)
			{
				num += CodedOutputStream.ComputeUInt32Size(4, Score);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgScore()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasRankNo)
		{
			output.WriteInt32(1, RankNo);
		}
		if (HasUserId)
		{
			output.WriteUInt32(2, UserId);
		}
		if (HasNickName)
		{
			output.WriteString(3, NickName);
		}
		if (HasScore)
		{
			output.WriteUInt32(4, Score);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasRankNo)
		{
			num ^= rankNo_.GetHashCode();
		}
		if (hasUserId)
		{
			num ^= userId_.GetHashCode();
		}
		if (hasNickName)
		{
			num ^= nickName_.GetHashCode();
		}
		if (hasScore)
		{
			num ^= score_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgScore msgScore))
		{
			return false;
		}
		if (hasRankNo != msgScore.hasRankNo || (hasRankNo && !rankNo_.Equals(msgScore.rankNo_)))
		{
			return false;
		}
		if (hasUserId != msgScore.hasUserId || (hasUserId && !userId_.Equals(msgScore.userId_)))
		{
			return false;
		}
		if (hasNickName != msgScore.hasNickName || (hasNickName && !nickName_.Equals(msgScore.nickName_)))
		{
			return false;
		}
		if (hasScore != msgScore.hasScore || (hasScore && !score_.Equals(msgScore.score_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgScore, Builder>.PrintField("rankNo", hasRankNo, rankNo_, writer);
		GeneratedMessageLite<MsgScore, Builder>.PrintField("userId", hasUserId, userId_, writer);
		GeneratedMessageLite<MsgScore, Builder>.PrintField("nickName", hasNickName, nickName_, writer);
		GeneratedMessageLite<MsgScore, Builder>.PrintField("score", hasScore, score_, writer);
	}

	public static MsgScore ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgScore ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgScore ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgScore ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgScore ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgScore ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgScore ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgScore ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgScore ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgScore ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgScore prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
