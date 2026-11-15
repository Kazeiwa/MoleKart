using System;
using System.Collections.Generic;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;

namespace RaceProtocol;

public sealed class MsgNotifyPlayerList : GeneratedMessageLite<MsgNotifyPlayerList, MsgNotifyPlayerList.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgNotifyPlayerList, Builder>
	{
		private MsgNotifyPlayerList result = new MsgNotifyPlayerList();

		protected override Builder ThisBuilder => this;

		protected override MsgNotifyPlayerList MessageBeingBuilt => result;

		public override MsgNotifyPlayerList DefaultInstanceForType => DefaultInstance;

		public IPopsicleList<MsgPlayer> PlayerListList => result.playerList_;

		public int PlayerListCount => result.PlayerListCount;

		public override Builder Clear()
		{
			result = new MsgNotifyPlayerList();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgNotifyPlayerList BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			result.playerList_.MakeReadOnly();
			MsgNotifyPlayerList msgNotifyPlayerList = result;
			result = null;
			return msgNotifyPlayerList;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgNotifyPlayerList)
			{
				return MergeFrom((MsgNotifyPlayerList)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgNotifyPlayerList other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.playerList_.Count != 0)
			{
				AddRange(other.playerList_, result.playerList_);
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
					MsgPlayer.Builder builder = MsgPlayer.CreateBuilder();
					input.ReadMessage(builder, extensionRegistry);
					AddPlayerList(builder.BuildPartial());
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

		public MsgPlayer GetPlayerList(int index)
		{
			return result.GetPlayerList(index);
		}

		public Builder SetPlayerList(int index, MsgPlayer value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.playerList_[index] = value;
			return this;
		}

		public Builder SetPlayerList(int index, MsgPlayer.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.playerList_[index] = builderForValue.Build();
			return this;
		}

		public Builder AddPlayerList(MsgPlayer value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.playerList_.Add(value);
			return this;
		}

		public Builder AddPlayerList(MsgPlayer.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.playerList_.Add(builderForValue.Build());
			return this;
		}

		public Builder AddRangePlayerList(IEnumerable<MsgPlayer> values)
		{
			AddRange(values, result.playerList_);
			return this;
		}

		public Builder ClearPlayerList()
		{
			result.playerList_.Clear();
			return this;
		}
	}

	public const int PlayerListFieldNumber = 1;

	private static readonly MsgNotifyPlayerList defaultInstance;

	private PopsicleList<MsgPlayer> playerList_ = new PopsicleList<MsgPlayer>();

	private int memoizedSerializedSize = -1;

	public static MsgNotifyPlayerList DefaultInstance => defaultInstance;

	public override MsgNotifyPlayerList DefaultInstanceForType => defaultInstance;

	protected override MsgNotifyPlayerList ThisMessage => this;

	public IList<MsgPlayer> PlayerListList => playerList_;

	public int PlayerListCount => playerList_.Count;

	public override bool IsInitialized
	{
		get
		{
			foreach (MsgPlayer playerList in PlayerListList)
			{
				if (!playerList.IsInitialized)
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
			foreach (MsgPlayer playerList in PlayerListList)
			{
				num += CodedOutputStream.ComputeMessageSize(1, playerList);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgNotifyPlayerList()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public MsgPlayer GetPlayerList(int index)
	{
		return playerList_[index];
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		foreach (MsgPlayer playerList in PlayerListList)
		{
			output.WriteMessage(1, playerList);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		foreach (MsgPlayer item in playerList_)
		{
			num ^= item.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgNotifyPlayerList msgNotifyPlayerList))
		{
			return false;
		}
		if (playerList_.Count != msgNotifyPlayerList.playerList_.Count)
		{
			return false;
		}
		for (int i = 0; i < playerList_.Count; i++)
		{
			if (!playerList_[i].Equals(msgNotifyPlayerList.playerList_[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgNotifyPlayerList, Builder>.PrintField("PlayerList", playerList_, writer);
	}

	public static MsgNotifyPlayerList ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgNotifyPlayerList ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgNotifyPlayerList prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
