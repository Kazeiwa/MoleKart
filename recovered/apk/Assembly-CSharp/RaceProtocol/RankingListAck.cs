using System;
using System.Collections.Generic;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;

namespace RaceProtocol;

public sealed class RankingListAck : GeneratedMessageLite<RankingListAck, RankingListAck.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<RankingListAck, Builder>
	{
		private RankingListAck result = new RankingListAck();

		protected override Builder ThisBuilder => this;

		protected override RankingListAck MessageBeingBuilt => result;

		public override RankingListAck DefaultInstanceForType => DefaultInstance;

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

		public IPopsicleList<MsgScore> RankListList => result.rankList_;

		public int RankListCount => result.RankListCount;

		public override Builder Clear()
		{
			result = new RankingListAck();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override RankingListAck BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			result.rankList_.MakeReadOnly();
			RankingListAck rankingListAck = result;
			result = null;
			return rankingListAck;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is RankingListAck)
			{
				return MergeFrom((RankingListAck)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(RankingListAck other)
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
			if (other.rankList_.Count != 0)
			{
				AddRange(other.rankList_, result.rankList_);
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
				case 50u:
				{
					MsgScore.Builder builder = MsgScore.CreateBuilder();
					input.ReadMessage(builder, extensionRegistry);
					AddRankList(builder.BuildPartial());
					continue;
				}
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

		public MsgScore GetRankList(int index)
		{
			return result.GetRankList(index);
		}

		public Builder SetRankList(int index, MsgScore value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.rankList_[index] = value;
			return this;
		}

		public Builder SetRankList(int index, MsgScore.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.rankList_[index] = builderForValue.Build();
			return this;
		}

		public Builder AddRankList(MsgScore value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.rankList_.Add(value);
			return this;
		}

		public Builder AddRankList(MsgScore.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.rankList_.Add(builderForValue.Build());
			return this;
		}

		public Builder AddRangeRankList(IEnumerable<MsgScore> values)
		{
			AddRange(values, result.rankList_);
			return this;
		}

		public Builder ClearRankList()
		{
			result.rankList_.Clear();
			return this;
		}
	}

	public const int ModeFieldNumber = 1;

	public const int TrackFieldNumber = 2;

	public const int RankTypeFieldNumber = 3;

	public const int PageNoFieldNumber = 4;

	public const int PageMaxFieldNumber = 5;

	public const int RankListFieldNumber = 6;

	private static readonly RankingListAck defaultInstance;

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

	private PopsicleList<MsgScore> rankList_ = new PopsicleList<MsgScore>();

	private int memoizedSerializedSize = -1;

	public static RankingListAck DefaultInstance => defaultInstance;

	public override RankingListAck DefaultInstanceForType => defaultInstance;

	protected override RankingListAck ThisMessage => this;

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

	public IList<MsgScore> RankListList => rankList_;

	public int RankListCount => rankList_.Count;

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
			foreach (MsgScore rankList in RankListList)
			{
				if (!rankList.IsInitialized)
				{
					return false;
				}
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
			foreach (MsgScore rankList in RankListList)
			{
				num += CodedOutputStream.ComputeMessageSize(6, rankList);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static RankingListAck()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public MsgScore GetRankList(int index)
	{
		return rankList_[index];
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
		foreach (MsgScore rankList in RankListList)
		{
			output.WriteMessage(6, rankList);
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
		foreach (MsgScore item in rankList_)
		{
			num ^= item.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is RankingListAck rankingListAck))
		{
			return false;
		}
		if (hasMode != rankingListAck.hasMode || (hasMode && !mode_.Equals(rankingListAck.mode_)))
		{
			return false;
		}
		if (hasTrack != rankingListAck.hasTrack || (hasTrack && !track_.Equals(rankingListAck.track_)))
		{
			return false;
		}
		if (hasRankType != rankingListAck.hasRankType || (hasRankType && !rankType_.Equals(rankingListAck.rankType_)))
		{
			return false;
		}
		if (hasPageNo != rankingListAck.hasPageNo || (hasPageNo && !pageNo_.Equals(rankingListAck.pageNo_)))
		{
			return false;
		}
		if (hasPageMax != rankingListAck.hasPageMax || (hasPageMax && !pageMax_.Equals(rankingListAck.pageMax_)))
		{
			return false;
		}
		if (rankList_.Count != rankingListAck.rankList_.Count)
		{
			return false;
		}
		for (int i = 0; i < rankList_.Count; i++)
		{
			if (!rankList_[i].Equals(rankingListAck.rankList_[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<RankingListAck, Builder>.PrintField("mode", hasMode, mode_, writer);
		GeneratedMessageLite<RankingListAck, Builder>.PrintField("track", hasTrack, track_, writer);
		GeneratedMessageLite<RankingListAck, Builder>.PrintField("rankType", hasRankType, rankType_, writer);
		GeneratedMessageLite<RankingListAck, Builder>.PrintField("pageNo", hasPageNo, pageNo_, writer);
		GeneratedMessageLite<RankingListAck, Builder>.PrintField("pageMax", hasPageMax, pageMax_, writer);
		GeneratedMessageLite<RankingListAck, Builder>.PrintField("RankList", rankList_, writer);
	}

	public static RankingListAck ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static RankingListAck ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static RankingListAck ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static RankingListAck ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static RankingListAck ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static RankingListAck ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static RankingListAck ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static RankingListAck ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static RankingListAck ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static RankingListAck ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(RankingListAck prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
