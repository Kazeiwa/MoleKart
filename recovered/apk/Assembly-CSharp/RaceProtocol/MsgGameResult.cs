using System;
using System.Collections.Generic;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;

namespace RaceProtocol;

public sealed class MsgGameResult : GeneratedMessageLite<MsgGameResult, MsgGameResult.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgGameResult, Builder>
	{
		private MsgGameResult result = new MsgGameResult();

		protected override Builder ThisBuilder => this;

		protected override MsgGameResult MessageBeingBuilt => result;

		public override MsgGameResult DefaultInstanceForType => DefaultInstance;

		public IPopsicleList<MsgPlayerResult> PlayerResultListList => result.playerResultList_;

		public int PlayerResultListCount => result.PlayerResultListCount;

		public override Builder Clear()
		{
			result = new MsgGameResult();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgGameResult BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			result.playerResultList_.MakeReadOnly();
			MsgGameResult msgGameResult = result;
			result = null;
			return msgGameResult;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgGameResult)
			{
				return MergeFrom((MsgGameResult)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgGameResult other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.playerResultList_.Count != 0)
			{
				AddRange(other.playerResultList_, result.playerResultList_);
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
					MsgPlayerResult.Builder builder = MsgPlayerResult.CreateBuilder();
					input.ReadMessage(builder, extensionRegistry);
					AddPlayerResultList(builder.BuildPartial());
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

		public MsgPlayerResult GetPlayerResultList(int index)
		{
			return result.GetPlayerResultList(index);
		}

		public Builder SetPlayerResultList(int index, MsgPlayerResult value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.playerResultList_[index] = value;
			return this;
		}

		public Builder SetPlayerResultList(int index, MsgPlayerResult.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.playerResultList_[index] = builderForValue.Build();
			return this;
		}

		public Builder AddPlayerResultList(MsgPlayerResult value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.playerResultList_.Add(value);
			return this;
		}

		public Builder AddPlayerResultList(MsgPlayerResult.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.playerResultList_.Add(builderForValue.Build());
			return this;
		}

		public Builder AddRangePlayerResultList(IEnumerable<MsgPlayerResult> values)
		{
			AddRange(values, result.playerResultList_);
			return this;
		}

		public Builder ClearPlayerResultList()
		{
			result.playerResultList_.Clear();
			return this;
		}
	}

	public const int PlayerResultListFieldNumber = 1;

	private static readonly MsgGameResult defaultInstance;

	private PopsicleList<MsgPlayerResult> playerResultList_ = new PopsicleList<MsgPlayerResult>();

	private int memoizedSerializedSize = -1;

	public static MsgGameResult DefaultInstance => defaultInstance;

	public override MsgGameResult DefaultInstanceForType => defaultInstance;

	protected override MsgGameResult ThisMessage => this;

	public IList<MsgPlayerResult> PlayerResultListList => playerResultList_;

	public int PlayerResultListCount => playerResultList_.Count;

	public override bool IsInitialized
	{
		get
		{
			foreach (MsgPlayerResult playerResultList in PlayerResultListList)
			{
				if (!playerResultList.IsInitialized)
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
			foreach (MsgPlayerResult playerResultList in PlayerResultListList)
			{
				num += CodedOutputStream.ComputeMessageSize(1, playerResultList);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgGameResult()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public MsgPlayerResult GetPlayerResultList(int index)
	{
		return playerResultList_[index];
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		foreach (MsgPlayerResult playerResultList in PlayerResultListList)
		{
			output.WriteMessage(1, playerResultList);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		foreach (MsgPlayerResult item in playerResultList_)
		{
			num ^= item.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgGameResult msgGameResult))
		{
			return false;
		}
		if (playerResultList_.Count != msgGameResult.playerResultList_.Count)
		{
			return false;
		}
		for (int i = 0; i < playerResultList_.Count; i++)
		{
			if (!playerResultList_[i].Equals(msgGameResult.playerResultList_[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgGameResult, Builder>.PrintField("PlayerResultList", playerResultList_, writer);
	}

	public static MsgGameResult ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgGameResult ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgGameResult ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgGameResult ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgGameResult ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgGameResult ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgGameResult ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgGameResult ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgGameResult ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgGameResult ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgGameResult prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
