using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class RankingListReq : GeneratedMessageLite<RankingListReq, RankingListReq.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<RankingListReq, Builder>
	{
		private RankingListReq result = new RankingListReq();

		protected override Builder ThisBuilder => this;

		protected override RankingListReq MessageBeingBuilt => result;

		public override RankingListReq DefaultInstanceForType => DefaultInstance;

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

		public bool HasPageNo => result.HasPageNo;

		[CLSCompliant(false)]
		public uint PageNo
		{
			get
			{
				return result.PageNo;
			}
			set
			{
				SetPageNo(value);
			}
		}

		public bool HasPageMax => result.HasPageMax;

		[CLSCompliant(false)]
		public uint PageMax
		{
			get
			{
				return result.PageMax;
			}
			set
			{
				SetPageMax(value);
			}
		}

		public override Builder Clear()
		{
			result = new RankingListReq();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override RankingListReq BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			RankingListReq rankingListReq = result;
			result = null;
			return rankingListReq;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is RankingListReq)
			{
				return MergeFrom((RankingListReq)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(RankingListReq other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasMode)
			{
				Mode = other.Mode;
			}
			if (other.HasTrack)
			{
				Track = other.Track;
			}
			if (other.HasRankType)
			{
				RankType = other.RankType;
			}
			if (other.HasPageNo)
			{
				PageNo = other.PageNo;
			}
			if (other.HasPageMax)
			{
				PageMax = other.PageMax;
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
					Mode = input.ReadUInt32();
					continue;
				case 16u:
					Track = input.ReadUInt32();
					continue;
				case 24u:
					RankType = input.ReadUInt32();
					continue;
				case 32u:
					PageNo = input.ReadUInt32();
					continue;
				case 40u:
					PageMax = input.ReadUInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
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

		[CLSCompliant(false)]
		public Builder SetPageNo(uint value)
		{
			result.hasPageNo = true;
			result.pageNo_ = value;
			return this;
		}

		public Builder ClearPageNo()
		{
			result.hasPageNo = false;
			result.pageNo_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetPageMax(uint value)
		{
			result.hasPageMax = true;
			result.pageMax_ = value;
			return this;
		}

		public Builder ClearPageMax()
		{
			result.hasPageMax = false;
			result.pageMax_ = 0u;
			return this;
		}
	}

	public const int ModeFieldNumber = 1;

	public const int TrackFieldNumber = 2;

	public const int RankTypeFieldNumber = 3;

	public const int PageNoFieldNumber = 4;

	public const int PageMaxFieldNumber = 5;

	private static readonly RankingListReq defaultInstance;

	private bool hasMode;

	private uint mode_;

	private bool hasTrack;

	private uint track_;

	private bool hasRankType;

	private uint rankType_;

	private bool hasPageNo;

	private uint pageNo_;

	private bool hasPageMax;

	private uint pageMax_;

	private int memoizedSerializedSize = -1;

	public static RankingListReq DefaultInstance => defaultInstance;

	public override RankingListReq DefaultInstanceForType => defaultInstance;

	protected override RankingListReq ThisMessage => this;

	public bool HasMode => hasMode;

	[CLSCompliant(false)]
	public uint Mode => mode_;

	public bool HasTrack => hasTrack;

	[CLSCompliant(false)]
	public uint Track => track_;

	public bool HasRankType => hasRankType;

	[CLSCompliant(false)]
	public uint RankType => rankType_;

	public bool HasPageNo => hasPageNo;

	[CLSCompliant(false)]
	public uint PageNo => pageNo_;

	public bool HasPageMax => hasPageMax;

	[CLSCompliant(false)]
	public uint PageMax => pageMax_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasMode)
			{
				return false;
			}
			if (!hasTrack)
			{
				return false;
			}
			if (!hasRankType)
			{
				return false;
			}
			if (!hasPageNo)
			{
				return false;
			}
			if (!hasPageMax)
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
			if (HasMode)
			{
				num += CodedOutputStream.ComputeUInt32Size(1, Mode);
			}
			if (HasTrack)
			{
				num += CodedOutputStream.ComputeUInt32Size(2, Track);
			}
			if (HasRankType)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, RankType);
			}
			if (HasPageNo)
			{
				num += CodedOutputStream.ComputeUInt32Size(4, PageNo);
			}
			if (HasPageMax)
			{
				num += CodedOutputStream.ComputeUInt32Size(5, PageMax);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static RankingListReq()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasMode)
		{
			output.WriteUInt32(1, Mode);
		}
		if (HasTrack)
		{
			output.WriteUInt32(2, Track);
		}
		if (HasRankType)
		{
			output.WriteUInt32(3, RankType);
		}
		if (HasPageNo)
		{
			output.WriteUInt32(4, PageNo);
		}
		if (HasPageMax)
		{
			output.WriteUInt32(5, PageMax);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasMode)
		{
			num ^= mode_.GetHashCode();
		}
		if (hasTrack)
		{
			num ^= track_.GetHashCode();
		}
		if (hasRankType)
		{
			num ^= rankType_.GetHashCode();
		}
		if (hasPageNo)
		{
			num ^= pageNo_.GetHashCode();
		}
		if (hasPageMax)
		{
			num ^= pageMax_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is RankingListReq rankingListReq))
		{
			return false;
		}
		if (hasMode != rankingListReq.hasMode || (hasMode && !mode_.Equals(rankingListReq.mode_)))
		{
			return false;
		}
		if (hasTrack != rankingListReq.hasTrack || (hasTrack && !track_.Equals(rankingListReq.track_)))
		{
			return false;
		}
		if (hasRankType != rankingListReq.hasRankType || (hasRankType && !rankType_.Equals(rankingListReq.rankType_)))
		{
			return false;
		}
		if (hasPageNo != rankingListReq.hasPageNo || (hasPageNo && !pageNo_.Equals(rankingListReq.pageNo_)))
		{
			return false;
		}
		if (hasPageMax != rankingListReq.hasPageMax || (hasPageMax && !pageMax_.Equals(rankingListReq.pageMax_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<RankingListReq, Builder>.PrintField("mode", hasMode, mode_, writer);
		GeneratedMessageLite<RankingListReq, Builder>.PrintField("track", hasTrack, track_, writer);
		GeneratedMessageLite<RankingListReq, Builder>.PrintField("rankType", hasRankType, rankType_, writer);
		GeneratedMessageLite<RankingListReq, Builder>.PrintField("pageNo", hasPageNo, pageNo_, writer);
		GeneratedMessageLite<RankingListReq, Builder>.PrintField("pageMax", hasPageMax, pageMax_, writer);
	}

	public static RankingListReq ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static RankingListReq ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static RankingListReq ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static RankingListReq ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static RankingListReq ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static RankingListReq ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static RankingListReq ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static RankingListReq ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static RankingListReq ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static RankingListReq ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(RankingListReq prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
