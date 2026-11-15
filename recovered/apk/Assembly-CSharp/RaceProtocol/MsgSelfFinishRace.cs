using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgSelfFinishRace : GeneratedMessageLite<MsgSelfFinishRace, MsgSelfFinishRace.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgSelfFinishRace, Builder>
	{
		private MsgSelfFinishRace result = new MsgSelfFinishRace();

		protected override Builder ThisBuilder => this;

		protected override MsgSelfFinishRace MessageBeingBuilt => result;

		public override MsgSelfFinishRace DefaultInstanceForType => DefaultInstance;

		public bool HasRank => result.HasRank;

		public MsgRank Rank
		{
			get
			{
				return result.Rank;
			}
			set
			{
				SetRank(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgSelfFinishRace();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgSelfFinishRace BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgSelfFinishRace msgSelfFinishRace = result;
			result = null;
			return msgSelfFinishRace;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgSelfFinishRace)
			{
				return MergeFrom((MsgSelfFinishRace)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgSelfFinishRace other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasRank)
			{
				MergeRank(other.Rank);
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
				{
					MsgRank.Builder builder = MsgRank.CreateBuilder();
					if (HasRank)
					{
						builder.MergeFrom(Rank);
					}
					input.ReadMessage(builder, extensionRegistry);
					Rank = builder.BuildPartial();
					break;
				}
				}
			}
		}

		public Builder SetRank(MsgRank value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasRank = true;
			result.rank_ = value;
			return this;
		}

		public Builder SetRank(MsgRank.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.hasRank = true;
			result.rank_ = builderForValue.Build();
			return this;
		}

		public Builder MergeRank(MsgRank value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			if (result.HasRank && result.rank_ != MsgRank.DefaultInstance)
			{
				result.rank_ = MsgRank.CreateBuilder(result.rank_).MergeFrom(value).BuildPartial();
			}
			else
			{
				result.rank_ = value;
			}
			result.hasRank = true;
			return this;
		}

		public Builder ClearRank()
		{
			result.hasRank = false;
			result.rank_ = MsgRank.DefaultInstance;
			return this;
		}
	}

	public const int RankFieldNumber = 1;

	private static readonly MsgSelfFinishRace defaultInstance;

	private bool hasRank;

	private MsgRank rank_ = MsgRank.DefaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgSelfFinishRace DefaultInstance => defaultInstance;

	public override MsgSelfFinishRace DefaultInstanceForType => defaultInstance;

	protected override MsgSelfFinishRace ThisMessage => this;

	public bool HasRank => hasRank;

	public MsgRank Rank => rank_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasRank)
			{
				return false;
			}
			if (!Rank.IsInitialized)
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
			if (HasRank)
			{
				num += CodedOutputStream.ComputeMessageSize(1, Rank);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgSelfFinishRace()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasRank)
		{
			output.WriteMessage(1, Rank);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasRank)
		{
			num ^= rank_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgSelfFinishRace msgSelfFinishRace))
		{
			return false;
		}
		if (hasRank != msgSelfFinishRace.hasRank || (hasRank && !rank_.Equals(msgSelfFinishRace.rank_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgSelfFinishRace, Builder>.PrintField("Rank", hasRank, rank_, writer);
	}

	public static MsgSelfFinishRace ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgSelfFinishRace ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgSelfFinishRace ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgSelfFinishRace ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgSelfFinishRace prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
