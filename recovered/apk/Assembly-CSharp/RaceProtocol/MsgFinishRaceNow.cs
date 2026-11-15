using System;
using System.Collections.Generic;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;

namespace RaceProtocol;

public sealed class MsgFinishRaceNow : GeneratedMessageLite<MsgFinishRaceNow, MsgFinishRaceNow.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgFinishRaceNow, Builder>
	{
		private MsgFinishRaceNow result = new MsgFinishRaceNow();

		protected override Builder ThisBuilder => this;

		protected override MsgFinishRaceNow MessageBeingBuilt => result;

		public override MsgFinishRaceNow DefaultInstanceForType => DefaultInstance;

		public IPopsicleList<MsgRank> RankListList => result.rankList_;

		public int RankListCount => result.RankListCount;

		public override Builder Clear()
		{
			result = new MsgFinishRaceNow();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgFinishRaceNow BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			result.rankList_.MakeReadOnly();
			MsgFinishRaceNow msgFinishRaceNow = result;
			result = null;
			return msgFinishRaceNow;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgFinishRaceNow)
			{
				return MergeFrom((MsgFinishRaceNow)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgFinishRaceNow other)
		{
			if (other == DefaultInstance)
			{
				return this;
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
				case 10u:
				{
					MsgRank.Builder builder = MsgRank.CreateBuilder();
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

		public MsgRank GetRankList(int index)
		{
			return result.GetRankList(index);
		}

		public Builder SetRankList(int index, MsgRank value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.rankList_[index] = value;
			return this;
		}

		public Builder SetRankList(int index, MsgRank.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.rankList_[index] = builderForValue.Build();
			return this;
		}

		public Builder AddRankList(MsgRank value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.rankList_.Add(value);
			return this;
		}

		public Builder AddRankList(MsgRank.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.rankList_.Add(builderForValue.Build());
			return this;
		}

		public Builder AddRangeRankList(IEnumerable<MsgRank> values)
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

	public const int RankListFieldNumber = 1;

	private static readonly MsgFinishRaceNow defaultInstance;

	private PopsicleList<MsgRank> rankList_ = new PopsicleList<MsgRank>();

	private int memoizedSerializedSize = -1;

	public static MsgFinishRaceNow DefaultInstance => defaultInstance;

	public override MsgFinishRaceNow DefaultInstanceForType => defaultInstance;

	protected override MsgFinishRaceNow ThisMessage => this;

	public IList<MsgRank> RankListList => rankList_;

	public int RankListCount => rankList_.Count;

	public override bool IsInitialized
	{
		get
		{
			foreach (MsgRank rankList in RankListList)
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
			foreach (MsgRank rankList in RankListList)
			{
				num += CodedOutputStream.ComputeMessageSize(1, rankList);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgFinishRaceNow()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public MsgRank GetRankList(int index)
	{
		return rankList_[index];
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		foreach (MsgRank rankList in RankListList)
		{
			output.WriteMessage(1, rankList);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		foreach (MsgRank item in rankList_)
		{
			num ^= item.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgFinishRaceNow msgFinishRaceNow))
		{
			return false;
		}
		if (rankList_.Count != msgFinishRaceNow.rankList_.Count)
		{
			return false;
		}
		for (int i = 0; i < rankList_.Count; i++)
		{
			if (!rankList_[i].Equals(msgFinishRaceNow.rankList_[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgFinishRaceNow, Builder>.PrintField("RankList", rankList_, writer);
	}

	public static MsgFinishRaceNow ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRaceNow ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgFinishRaceNow ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgFinishRaceNow ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgFinishRaceNow prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
